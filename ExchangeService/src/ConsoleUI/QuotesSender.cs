using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleUI
{
    public class QuotesSender : BackgroundService
    {
        private readonly ILogger<QuotesSender> _logger;
        private readonly IQuotesService _quotesService;

        public QuotesSender(ILogger<QuotesSender> logger, IQuotesService quotesService)
        {
            _logger = logger;
            _quotesService = quotesService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                return base.StartAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to start quotes sender");
            }
            
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                return base.StartAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to stop quotes sender");
            }
            
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _quotesService.Send();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send quotes");
            }
        }
    }
}