using Carter;
using Shared.Library.Behaviours;

var builder = WebApplication.CreateBuilder(args);

// Add services to container (DI)
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});


builder.Services.AddMarten(options =>
{
   options.Connection(builder.Configuration.GetConnectionString("Database")!); 
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();