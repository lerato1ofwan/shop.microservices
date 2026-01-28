namespace Ordering.Application.Data.Interfaces;

public interface IOrderRepository
{
    Task<Guid> AddAsync(Order order);
    Task<bool> DeleteAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order> GetOrderById(Guid id, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Order order);
}