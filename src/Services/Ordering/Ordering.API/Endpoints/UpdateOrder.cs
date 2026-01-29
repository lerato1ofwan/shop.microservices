using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders",
            async (UpdateOrderRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<UpdateOrderResponse>();

                return Results.Ok(new UpdateOrderResponse(response.IsSuccess));
            })
            .WithName("UpdateOrder")
            .WithSummary("Updates an order.")
            .WithDescription("Updates an order.")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}