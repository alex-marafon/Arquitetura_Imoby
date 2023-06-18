using Custom_Architecture.Interfaces.Services;
using Custom_Architecture.Services;

namespace Custom_Architecture.Providers;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
