namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler
    (IOrderRepository orderRepository)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        // Get order, check if null return not found exception
        var order = await orderRepository.GetOrderById(command.OrderId, cancellationToken);
        if(order is null)
            throw new OrderNotFoundException(command.OrderId);

        // Delete
        var deleted = await orderRepository.DeleteAsync(order, cancellationToken);

        // Return success indicator
        return new DeleteOrderResult(deleted);
    }
}