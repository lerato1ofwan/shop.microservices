using Discount.Grpc;

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
    IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoService) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await ApplyDiscount(command, cancellationToken);

        var storedBasket = await basketRepository.StoreBasketAsync(command.Cart, cancellationToken);

        return new StoreBasketResult(storedBasket.UserName);
    }

    private async Task ApplyDiscount(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.Cart.Items)
        {
            var discountRequest = new GetDiscountRequest { ProductName = item.ProductName };
            var coupon = await discountProtoService.GetDiscountAsync(discountRequest, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}