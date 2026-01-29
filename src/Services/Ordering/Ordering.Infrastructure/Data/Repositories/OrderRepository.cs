using Shared.Library.Pagination;

namespace Ordering.Infrastructure.Data.Repositories;

public class OrderRepository
    (ApplicationDbContext context)
    : IOrderRepository
{
    public async Task<Guid> AddAsync(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        return order.Id.Value;
    }

    public async Task<bool> DeleteAsync(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Order> GetOrderById(Guid id, CancellationToken cancellationToken = default)
    {
        var orderId = OrderId.Of(id);
        var order = await context.Orders.FindAsync([orderId], cancellationToken: cancellationToken);

        return order!;
    }

    public Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        return context.Orders.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PaginatedResult<Order>> GetPaginatedOrdersAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken)
    {
        var pageIndex = paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize;

        var query = await context.Orders
            .AsNoTracking()
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await context.Orders.CountAsync(cancellationToken);

        return new PaginatedResult<Order>(
            pageIndex,
            pageSize,
            totalCount,
            query
        );
    }

    public async Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await context.Orders
            .AsNoTracking()
            .Where(x => x.CustomerId == CustomerId.Of(customerId))
            .OrderBy(x => x.OrderName.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Order>> GetOrdersByNameAsync(string orderName, CancellationToken cancellationToken)
    {
        return await context.Orders
            .AsNoTracking()
            .Where(x => x.OrderName.Value.Contains(orderName))
            .OrderBy(x => x.OrderName.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();

        return true;
    }
}