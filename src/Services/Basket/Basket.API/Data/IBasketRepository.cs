namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
}

public class CacheBasketRepository : IBasketRepository
{
    // private readonly IDistributedCache _cache;
    public CacheBasketRepository(
    // IDistributedCache cache
    )
    {
        // _cache = cache;
    }
    public async Task<ShoppingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        return basket;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        return false;
    }
}