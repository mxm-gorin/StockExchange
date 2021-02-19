using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IQuotesService, QuotesService>();
            
            return services;
        }
        
        public static void AddConfig<TConfig>(this IServiceCollection services, IConfiguration configuration)
            where TConfig : class
        {
            var type = typeof(TConfig);
            var section = configuration.GetSection(type.Name);

            services.Configure<TConfig>(section);
        }
    }
}