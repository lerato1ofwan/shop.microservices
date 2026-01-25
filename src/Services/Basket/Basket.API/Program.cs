var builder = WebApplication.CreateBuilder(args);

// Add services to container (DI)
var assembly = typeof(Program).Assembly;

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
