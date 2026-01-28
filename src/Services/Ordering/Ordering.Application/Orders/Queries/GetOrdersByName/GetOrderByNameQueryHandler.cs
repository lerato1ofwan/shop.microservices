using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrdersByNameQueryHandler
    (IOrderRepository orderRepository)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersByNameAsync(query.OrderName, cancellationToken);
        
        var results = orders.ToOrderDtoList();
        
        return new GetOrdersByNameResult(results);
    }
}