
namespace Ordering.Domain.Abstractions;

public interface IAggregate<T> : IAggregate, IEntity<T>
    where T : notnull
{ }

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    IDomainEvent[] ClearDomainEvents();
}