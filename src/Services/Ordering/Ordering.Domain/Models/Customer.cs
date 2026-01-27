namespace Ordering.Domain.Models;

/// <summary>
/// Represents a customer in the ordering domain (Customer entity)
/// </summary>
public class Customer : Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    #region Behaviours for the rich domain model (domain methods)
    
    /// <summary>
    /// Creates a new Customer entity instance
    /// </summary>
    /// <param name="id">The CustomerId value object</param>
    /// <param name="name">The name of the customer</param>
    /// <param name="email">The email address of the customer</param>
    /// <returns>A newly created Customer entity instance</returns>
    public static Customer Create(CustomerId id, string name, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

        var customer = new Customer
        {
            Id = id,
            Name = name,
            Email = email
        };

        return customer;
    }
    
    #endregion
}