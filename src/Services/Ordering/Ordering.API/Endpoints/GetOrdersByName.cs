using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

// public record GetOrdersByNameRequest(string orderName);

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}",
            async (string orderName, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetOrdersByNameQuery(orderName);
                var result = await sender.Send(query, cancellationToken);

                var response = result.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByName")
            .WithSummary("Gets orders by order name.")
            .WithDescription("Gets orders by order name.")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}