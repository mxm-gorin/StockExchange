using System;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Histogram;
using Core.Interfaces;

namespace Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ICacheService _cacheService;
        private static readonly IMetricsRoot Metrics = new MetricsBuilder().Build();

        public StatisticsService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public void UpdateQuotes(int number)
        {
            Metrics.Measure.Histogram.Update(new HistogramOptions {Name = $"{DateTime.UtcNow}"}, number);
        }

        public Task<double> GetQuotesAverage()
        {
            return Task.FromResult(Metrics.Snapshot.Get().Contexts
                .FirstOrDefault()?.Histograms
                .LastOrDefault()?.Value.Mean ?? 0);
            return Task.FromResult(_cacheService.QuotesAverage);
        }

        public Task<double> GetQuotesDeviation()
        {
            return Task.FromResult(
                Metrics.Snapshot.Get().Contexts
                    .FirstOrDefault()?.Histograms
                    .LastOrDefault()?.Value.StdDev ?? 0);
            return Task.FromResult(_cacheService.QuotesDeviation);
        }

        public Task<double> GetQuotesMode()
        {
            return Task.FromResult(Metrics.Snapshot.Get()
                .Contexts.FirstOrDefault()?
                .Histograms.LastOrDefault()?.Value.Mean ?? 0);
            return Task.FromResult(_cacheService.QuotesMode);
        }

        public Task<double> GetQuotesMedian()
        {
            return Task.FromResult(
                Metrics.Snapshot.Get()
                    .Contexts.FirstOrDefault()?
                    .Histograms.LastOrDefault()?
                    .Value.Median ?? 0);

            return Task.FromResult(_cacheService.QuotesMedian);
        }

        public Task<long> GetLostMessagesCount()
        {
            return Task.FromResult(_cacheService.LostMessagesCount);
        }
    }

}