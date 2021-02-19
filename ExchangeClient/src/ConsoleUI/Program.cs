using System;
using System.Threading.Tasks;
using ConsoleUI.Commands;
using ConsoleUI.Interfaces;
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
        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting application...");
                var host = CreateHostBuilder(args).Build();
                await host.Services.GetRequiredService<Application>().RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start. {ex}");
            }
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
                    services.AddCore();
                    services.AddInfrastructure();
                    services.AddTransient<Application>();
                    services.AddTransient<ICommandFactory, CommandFactory>();
                    services.AddConfig<QuotesReceiverConfig>(hostContext.Configuration);
                    services.AddConfig<UdpConfig>(hostContext.Configuration);
                });
    }
}