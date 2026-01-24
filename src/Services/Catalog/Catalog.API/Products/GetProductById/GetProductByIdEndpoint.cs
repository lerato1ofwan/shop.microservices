
namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}",
            async (Guid id, ISender sender) =>
            {
                var query = new GetProductByIdQuery(id);

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductByIdResponse>();

                return response;
            })
            .WithName("GetProductById")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Get product by id")
            .WithDescription("Get product by id");
    }
}