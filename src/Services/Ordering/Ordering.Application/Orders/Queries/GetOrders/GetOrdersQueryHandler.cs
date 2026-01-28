using Shared.Library.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler
    (IOrderRepository orderRepository)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetPaginatedOrdersAsync(query.PaginationRequest, cancellationToken);

        var results = new PaginatedResult<OrderDto>(
            orders.PageIndex,
            orders.PageSize,
            orders.Count,
            orders.Data.ToOrderDtoList()
        );

        return new GetOrdersResult(results);
    }
}