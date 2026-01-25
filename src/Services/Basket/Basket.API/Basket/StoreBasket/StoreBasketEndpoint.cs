namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(
    ShoppingCart Cart);

public record StoreBasketResponse(
    string UserName
);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket",
            async (StoreBasketRequest request, ISender sender) =>
            {
                var command = new StoreBasketCommand(request.Cart);

                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("StoreBasket")
            .WithSummary("Stores a shopping basket for a user.")
            .WithDescription("Stores a shopping basket for a user.")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}