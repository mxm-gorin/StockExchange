using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IQuotesRepository, RandomQuotesRepository>();
            services.AddTransient<INotificationService, UdpNotificationService>();
            
            return services;
        }
    }
}