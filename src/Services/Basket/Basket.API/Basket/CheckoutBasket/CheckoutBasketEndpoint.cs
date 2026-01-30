namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(
    BasketCheckoutDto BasketCheckoutDto
);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout",
            async (CheckoutBasketRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();

                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<CheckoutBasketResponse>();

                return response;
            })
            .WithName("CheckoutBasket")
            .WithSummary("CheckoutBasket")
            .WithDescription("Checkout basket")
            .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}