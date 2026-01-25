
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryRequest(
    int? PageNumber = 1,
    int? PageSize = 10);

public record GetProductByCategoryResponse(
    IList<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (string category, [AsParameters] GetProductByCategoryRequest request, ISender sender) =>
            {
                var query = new GetProductByCategoryQuery(category, request.PageNumber, request.PageSize);

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
