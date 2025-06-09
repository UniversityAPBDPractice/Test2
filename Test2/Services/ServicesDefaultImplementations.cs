namespace Test2.Services;

public static class ServicesDefaultImplementations
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}