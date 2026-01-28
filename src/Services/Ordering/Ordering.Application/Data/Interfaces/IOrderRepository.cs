using Shared.Library.Pagination;

namespace Ordering.Application.Data.Interfaces;

public interface IOrderRepository
{
    Task<Guid> AddAsync(Order order);
    Task<bool> DeleteAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order> GetOrderById(Guid id, CancellationToken cancellationToken = default);
    Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken);
    Task<PaginatedResult<Order>> GetPaginatedOrdersAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken);
    Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
    Task<List<Order>> GetOrdersByNameAsync(string orderName, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Order order);
}