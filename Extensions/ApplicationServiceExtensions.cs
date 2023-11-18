using Whitees.Helpers;
using Whitees.Interfaces;
using Whitees.Repositories;
using Whitees.Services;


namespace Whitees.Extensions;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration config)
    {

        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddCloudscribePagination();

        return services;
    }
}
