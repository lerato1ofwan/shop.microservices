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

    public async Task<bool> UpdateAsync(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();

        return true;
    }
}