namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler
    (IOrderRepository orderRepository)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // Create order entity from DTO
        var order = CreateNewOrder(command.Order);
        
        // Save order to database
        await orderRepository.AddAsync(order);

        // Return result
        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.Province,
            orderDto.ShippingAddress.PostalCode);

        var billingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.Province,
            orderDto.BillingAddress.PostalCode);

        var payment = Payment.Of(
            orderDto.Payment.CardNumber,
            orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration,
            orderDto.Payment.Cvv,
            orderDto.Payment.PaymentMethod);

        var newOrder = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: shippingAddress,
            billingAddress: billingAddress,
            payment: payment);

        foreach (var itemDto in orderDto.OrderItems)
        {
            newOrder.Add(ProductId.Of(itemDto.ProductId), itemDto.Quantity, itemDto.Price);
        }

        return newOrder;
    }
}