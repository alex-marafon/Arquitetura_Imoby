using Custom_Architecture.Interfaces.Repositories;
using Custom_Architecture.Repositories;

namespace Custom_Architecture.Providers;

public static class RepositoriesConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();

        return services;
    }
}