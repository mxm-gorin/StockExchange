using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICacheService
    {
        public long LostMessagesCount { get; set; }
        public double QuotesAverage { get; set;}
        public double QuotesDeviation { get; set;}
        public double QuotesMode { get; set;}
        public double QuotesMedian { get; set;}
        Task AddLostMessages(long count);
    }
}