namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(MapToOrderDto).ToList();
    }
    
    public static OrderDto ToOrderDto(this Order order)
    {
        return MapToOrderDto(order);
    }

    private static OrderDto MapToOrderDto(Order order)
    {
        return new OrderDto(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            OrderItems: order.OrderItems.Select(oi => new OrderItemDto(
                OrderId: oi.OrderId.Value,
                ProductId: oi.ProductId.Value,
                Quantity: oi.Quantity,
                Price: oi.Price)).ToList(),
            ShippingAddress: new AddressDto(
                FirstName: order.ShippingAddress.FirstName,
                LastName: order.ShippingAddress.LastName,
                EmailAddress: order.ShippingAddress.EmailAddress!,
                AddressLine: order.ShippingAddress.AddressLine,
                Country: order.ShippingAddress.Country,
                Province: order.ShippingAddress.Province,
                PostalCode: order.ShippingAddress.PostalCode),
            BillingAddress: new AddressDto(
                FirstName: order.BillingAddress.FirstName,
                LastName: order.BillingAddress.LastName,
                EmailAddress: order.BillingAddress.EmailAddress!,
                AddressLine: order.BillingAddress.AddressLine,
                Country: order.BillingAddress.Country,
                Province: order.BillingAddress.Province,
                PostalCode: order.BillingAddress.PostalCode),
            Payment: new PaymentDto(
                CardName: order.Payment.CardName!,
                CardNumber: order.Payment.CardNumber,
                Expiration: order.Payment.Expiration,
                Cvv: order.Payment.CVV,
                PaymentMethod: order.Payment.PaymentMethod),
            Status: order.Status
        );
    }
}