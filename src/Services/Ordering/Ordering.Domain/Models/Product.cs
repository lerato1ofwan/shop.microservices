namespace Ordering.Domain.Models;

/// <summary>
/// Represents a product in the ordering domain (Product entity)
/// </summary>
public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    #region Behaviours for the rich domain model (domain methods)

    /// <summary>
    /// Creates a new Product entity instance
    /// </summary>
    /// <param name="id">The ProductId value object</param>
    /// <param name="name">The name of the product</param>
    /// <param name="price">The price of the product</param>
    /// <returns>A newly created Product entity instance</returns>
    public static Product Create(ProductId id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

        var product = new Product
        {
            Id = id,
            Name = name,
            Price = price
        };

        return product;
    }

    #endregion
}