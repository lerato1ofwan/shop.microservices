using MediatR;

namespace Shared.Library.CQRS;

public interface ICommand : IQuery<Unit> { }
public interface ICommand<out TResponse> : IRequest<TResponse> { }