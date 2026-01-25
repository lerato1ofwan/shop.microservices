
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(
    IList<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (string category, ISender sender) =>
            {
                var query = new GetProductByCategoryQuery(category);

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductByCategoryResponse>();
                
                return Results.Ok(response);
            })
            .WithName("GetProductsByCategory")
            .WithSummary("Get Products by Category Id")
            .WithDescription("Get all products that belong to a specific category by Category Id")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
