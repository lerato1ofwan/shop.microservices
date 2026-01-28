namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    OrderDto Order
) : ICommand<CreateOrderCommandResult>;

public record CreateOrderCommandResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull().WithMessage("Order is required.");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName is required.");
        RuleFor(x => x.Order.CustomerId).NotEqual(Guid.Empty).WithMessage("CustomerId is required.");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order items should not be empty.");
    }
}