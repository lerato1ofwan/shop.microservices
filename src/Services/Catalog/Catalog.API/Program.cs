using Carter;
using Catalog.API.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);

// Add services to container (DI)
builder.Services.AddCarter();

var app = builder.Build();

app.Run();