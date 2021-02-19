using Core;
using Core.Configs;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configurationBuilder) =>
                {
                    configurationBuilder
                        .AddXmlFile("appconfig.xml", false, true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddConfig<UdpConfig>(hostContext.Configuration);
                    services.AddConfig<QuotesConfig>(hostContext.Configuration);
                    services.AddCore();
                    services.AddInfrastructure();
                    services.AddHostedService<QuotesSender>();
                });

    }
}