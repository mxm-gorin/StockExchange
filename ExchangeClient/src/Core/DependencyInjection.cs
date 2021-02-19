using Core.Interfaces;
using Core.Jobs;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IReceiveExchangeQuotesJob, ReceiveExchangeQuotesJob>();
            services.AddTransient<ICalculateQuoteStatisticsJob, CalculateQuoteStatisticsJob>();
            services.AddTransient<IJobService, JobService>();

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