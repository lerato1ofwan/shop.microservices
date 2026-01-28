using Ordering.Domain.Models;

namespace Ordering.Application.Data.Interfaces;

public interface IOrderRepository
{
    Task<Guid> AddAsync(Order order);
}