using Shared.Library.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

// public record GetOrdersRequest(PaginationRequest Request);

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders",
            async ([AsParameters] PaginationRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetOrdersQuery(request);
                var result = await sender.Send(query, cancellationToken);

                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .WithSummary("Gets all orders.")
            .WithDescription("Gets all orders.")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}