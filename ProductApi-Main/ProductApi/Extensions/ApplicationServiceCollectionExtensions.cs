using Microsoft.Extensions.DependencyInjection;
using ProductApi.Helpers;
using ProductApi.Services;

namespace ProductApi.Extensions
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(typeof(AutomapperProfiles).Assembly);

            return services;
        }
    }
}
