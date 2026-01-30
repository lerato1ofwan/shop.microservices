using MassTransit;
using Shared.Library.Messaging.Events;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(
    BasketCheckoutDto BasketCheckoutDto
) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto is required");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class CheckoutBasketCommandHandler 
    (IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);
        if (basket is null)
            return new CheckoutBasketResult(false);

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        // @Todo: Good place to use the outbox pattern here
        // To ensure consistency so we can publish and delete both operations
        // The outbox can be used to delete from the basket or vice versa
        // Investigate best approach 
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);        
    }
}