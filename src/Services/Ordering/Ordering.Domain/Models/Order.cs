namespace Ordering.Domain.Models;

/// <summary>
/// Represents an order in the ordering domain (Order enitity and aggregate)
/// </summary>
public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    #region Behaviours for the rich domain model (domain methods)

    /// <summary>
    /// Creates a new Order entity instance
    /// </summary>
    /// <param name="id">The OrderId value object</param>
    /// <param name="customerId">The CustomerId value object</param>
    /// <param name="orderName">The OrderName value object</param>
    /// <param name="shippingAddress">The shipping address</param>
    /// <param name="billingAddress">The billing address</param>
    /// <param name="payment">The payment information</param>
    /// <returns>A newly created Order entity instance</returns>
    public static Order Create(OrderId id, CustomerId customerId, OrderName orderName,
        Address shippingAddress, Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    /// <summary>
    /// Updates the Order entity instance
    /// </summary>
    /// <param name="orderName">The name of the order</param>
    /// <param name="shippingAddress">The shipping address</param>
    /// <param name="billingAddress">The billing address</param>
    /// <param name="payment">The payment information</param>
    public void Update(OrderName orderName, Address shippingAddress,
        Address billingAddress, Payment payment)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }

    /// <summary>
    /// Removes an OrderItem from the Order aggregate
    /// </summary>
    /// <param name="productId"></param>
    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(oi => oi.ProductId == productId);
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }

    /// <summary>
    /// Adds an OrderItem to the Order aggregate
    /// </summary>
    /// <param name="orderItem"></param>
    public void AddOrderItem(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
    }

    #endregion
}