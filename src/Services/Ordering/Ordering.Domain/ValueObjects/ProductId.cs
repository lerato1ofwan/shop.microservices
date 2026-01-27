namespace Ordering.Domain.ValueObjects;

public record ProductId 
{
    public Guid Value { get; }

    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid value)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(value));
        if (value == Guid.Empty)
            throw new DomainException("ProductId value cannot be empty.", nameof(value));

        return new ProductId(value);
    }
}