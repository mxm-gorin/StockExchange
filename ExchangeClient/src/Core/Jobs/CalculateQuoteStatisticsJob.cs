using System;
using System.Threading.Tasks;
using Core.Configs;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.Jobs
{
    public class CalculateQuoteStatisticsJob : BaseJob, ICalculateQuoteStatisticsJob
    {
        private readonly ILogger<CalculateQuoteStatisticsJob> _logger;
        private readonly IQuotesRepository _quotesRepository;
        private readonly ICacheService _cacheService;

        public CalculateQuoteStatisticsJob(
            ILogger<CalculateQuoteStatisticsJob> logger,
            IQuotesRepository quotesRepository,
            ICacheService cacheService,
            IOptions<QuotesReceiverConfig> receiverConfig)
            : base(TimeSpan.FromMilliseconds(receiverConfig.Value.WorkDelayMs * 0.8))
        {
            _logger = logger;
            _quotesRepository = quotesRepository;
            _cacheService = cacheService;
        }

        protected override async Task ExecuteAsync()
        {
            try
            {
                var deviationTask = _quotesRepository.GetDeviationAsync();
                var medianTask = _quotesRepository.GetMedianAsync();
                var modeTask = _quotesRepository.GetModeAsync();
                await Task.WhenAll(deviationTask, medianTask, modeTask);
                _cacheService.QuotesAverage = _quotesRepository.GetMean();
                _cacheService.QuotesDeviation = deviationTask.Result;
                _cacheService.QuotesMedian = medianTask.Result;
                _cacheService.QuotesMode = modeTask.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to calculate statistics");
            }
        }
    }
}