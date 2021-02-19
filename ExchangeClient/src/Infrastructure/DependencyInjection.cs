
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IExchangeService, ExchangeService>();
            services.AddTransient<IQuotesRepository, QuotesRepository>();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}