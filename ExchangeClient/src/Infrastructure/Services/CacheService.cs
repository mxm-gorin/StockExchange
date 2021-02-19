using System.Threading.Tasks;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        public long LostMessagesCount { get; set; }
        public double QuotesAverage { get; set; }
        public double QuotesDeviation { get; set; }
        public double QuotesMode { get; set; }
        public double QuotesMedian { get; set; }

        public Task AddLostMessages(long count)
        {
            LostMessagesCount += count;

            return Task.CompletedTask;
        }
    }
}