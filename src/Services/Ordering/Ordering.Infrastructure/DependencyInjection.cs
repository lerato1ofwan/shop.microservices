using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // services.AddDbContext<OrderingDbContext>(options =>
            // {
            //     options.UseSqlServer(
            //          configuration.GetConnectionString("Database"),
            //         sqlOptions => sqlOptions.MigrationsAssembly(typeof(OrderingDbContext).Assembly.FullName));
            // });
            // services.AddScoped<IApplicationDbContext, OrderingDbContext>();

            return services;
        }
    }
}
