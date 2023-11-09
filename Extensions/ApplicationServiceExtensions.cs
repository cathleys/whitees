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
        services.AddScoped<IShirtRepository, ShirtRepository>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();

        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddCloudscribePagination();

        return services;
    }
}
