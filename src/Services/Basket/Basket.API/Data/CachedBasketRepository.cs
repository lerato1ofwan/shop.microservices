using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Data;

public class CachedBasketRepository(
    IBasketRepository basketRepository,
    IDistributedCache cache) : IBasketRepository
{
    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasketAsync(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }

    public async Task<ShoppingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrWhiteSpace(cachedBasket))
            return JsonConvert.DeserializeObject<ShoppingCart>(cachedBasket)!;

        var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);
        if (basket is not null)
        {
            var serializedBasket = JsonConvert.SerializeObject(basket);
            await cache.SetStringAsync(userName, serializedBasket, cancellationToken);
        }

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasketAsync(basket, cancellationToken);

        var serializedBasket = JsonConvert.SerializeObject(basket);
        await cache.SetStringAsync(basket.UserName, serializedBasket, cancellationToken);

        return basket;
    }
}