namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(
    ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(
    string UserName
);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Shopping cart cannot be null.");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User name cannot be empty.");
    }
}

public class StoreBasketHandler(
    IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        var updatedBasket = await basketRepository.UpdateBasketAsync(cart);

        return new StoreBasketResult("swm");
    }
}