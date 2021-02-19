using System;
using System.Threading.Tasks;
using Core.Configs;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.Jobs
{
    public class ReceiveExchangeQuotesJob : BaseJob, IReceiveExchangeQuotesJob
    {
        private readonly ILogger<ReceiveExchangeQuotesJob> _logger;
        private readonly IExchangeService _exchangeService;
        private readonly IQuotesRepository _quotesRepository;

        public ReceiveExchangeQuotesJob(ILogger<ReceiveExchangeQuotesJob> logger, 
            IExchangeService exchangeService,
            IOptions<QuotesReceiverConfig> config,
            IQuotesRepository quotesRepository)
            : base(TimeSpan.FromMilliseconds(config.Value.WorkDelayMs))
        {
            _logger = logger;
            _exchangeService = exchangeService;
            _quotesRepository = quotesRepository;
        }

        protected override async Task ExecuteAsync()
        {
            try
            {
                var quotes = await _exchangeService.ReceiveQuotesAsync();
                _quotesRepository.Add(quotes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save quotes");
            }
        }
    }
}