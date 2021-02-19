using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class JobService : IJobService
    {
        private readonly ILogger<JobService> _logger;
        private readonly IReceiveExchangeQuotesJob _receiveExchangeQuotesJob;
        private readonly ICalculateQuoteStatisticsJob _calculateQuoteStatisticsJob;

        public JobService(ILogger<JobService> logger,
            IReceiveExchangeQuotesJob receiveExchangeQuotesJob,
            ICalculateQuoteStatisticsJob calculateQuoteStatisticsJob)
        {
            _logger = logger;
            _receiveExchangeQuotesJob = receiveExchangeQuotesJob;
            _calculateQuoteStatisticsJob = calculateQuoteStatisticsJob;
        }
        
        public void StartJobs()
        {
            _receiveExchangeQuotesJob.Start();
            _calculateQuoteStatisticsJob.Start();
        }

        public void StopJobs()
        {
            _receiveExchangeQuotesJob.Stop();
            _calculateQuoteStatisticsJob.Stop();
        }
    }
}