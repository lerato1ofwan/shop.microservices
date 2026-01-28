namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQueryHandler
    (IOrderRepository orderRepository)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersByCustomerIdAsync(request.CustomerId, cancellationToken);

        var results = orders.ToOrderDtoList();

        return new GetOrdersByCustomerResult(results);
    }
}