namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler
    (IOrderRepository orderRepository)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        // Get order, check if null return not found exception
        var order = await orderRepository.GetOrderById(command.Order.Id);
        if(order is null)
            throw new OrderNotFoundException(command.Order.Id);

        // Override all values
        UpdateOrderWithNewValues(order,  command.Order);

        // Update
        var updated = await orderRepository.UpdateAsync(order);

        // Return success indicator
        return new UpdateOrderResult(updated);
    }

    private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var updatedShippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.Province,
            orderDto.ShippingAddress.PostalCode);

        var updatedBillingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.Province,
            orderDto.BillingAddress.PostalCode);

        var updatedPayment = Payment.Of(
            orderDto.Payment.CardNumber,
            orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration,
            orderDto.Payment.Cvv,
            orderDto.Payment.PaymentMethod);

        order.Update(order.OrderName, updatedShippingAddress, updatedBillingAddress, updatedPayment);
    }
}