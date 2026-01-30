using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;
using Shared.Library.Messaging.Events;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler
    (ILogger<BasketCheckoutEventHandler> logger, ISender sender)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Integrated event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var createOrderCommand = MapToCreateOrderCommand(context.Message);
        await sender.Send(createOrderCommand);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: new AddressDto(
                message.FirstName,
                message.LastName,
                message.EmailAddress,
                message.AddressLine,
                message.Country,
                message.Province,
                message.PostalCode),
            BillingAddress: new AddressDto(
                message.FirstName,
                message.LastName,
                message.EmailAddress,
                message.AddressLine,
                message.Country,
                message.Province,
                message.PostalCode),
            Payment: new PaymentDto(
                message.CardName!,
                message.CardNumber,
                message.Expiration,
                message.Cvv,
                message.PaymentMethod),
            Status: OrderStatus.Pending,
            OrderItems: [
                new OrderItemDto(
                    OrderId: orderId,
                    ProductId: new Guid("a37d162b-1aa9-4c93-a0ad-c36355e7393f"), // @Todo: These should be coming incoming message 
                    Quantity: 2,
                    Price: 3899),
                new OrderItemDto(
                    OrderId: orderId,
                    ProductId: new Guid("4bb93446-f17b-4c0a-b2de-b1d6e1e34ff0"), // @Todo: These should be coming incoming message 
                    Quantity: 1,
                    Price: 159)
                ]
        );

        return new CreateOrderCommand(orderDto);
    }
}