namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid value)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(value));
        if (value == Guid.Empty)
            throw new DomainException("OrderId value cannot be empty.", nameof(value));

        return new OrderId(value);
    }
}