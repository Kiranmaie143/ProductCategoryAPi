using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductApi.Data;

namespace ProductApi.Extensions
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProductDataSettings>(configuration.GetSection(nameof(ProductDataSettings)));

            services.AddSingleton<IProductDataSettings>(sp =>
                sp.GetRequiredService<IOptions<ProductDataSettings>>().Value);

            services.AddSingleton<IMongoClient>(s =>
                new MongoClient(configuration.GetSection("ProductDataSettings")["ConnectionString"]));

            return services;
        }
    }
}
