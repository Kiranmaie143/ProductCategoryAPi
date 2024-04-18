using Microsoft.Extensions.DependencyInjection;

namespace ProductApi.Extensions
{
    public static class SecurityServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyOrigin();
                });
            });

            return services;
        }
    }
}
