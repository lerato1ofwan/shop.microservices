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
}