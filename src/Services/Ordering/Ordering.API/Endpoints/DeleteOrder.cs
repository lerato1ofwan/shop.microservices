using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

// public record DeleteOrderRequest(Guid Id);

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}",
            async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new DeleteOrderCommand(id);
                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .WithSummary("Deletes an order.")
            .WithDescription("Deletes an order.")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}