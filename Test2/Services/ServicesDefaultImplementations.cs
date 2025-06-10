using Test2.Services.Abstractions;

namespace Test2.Services;

public static class ServicesDefaultImplementations
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();

        return services;
    }
}