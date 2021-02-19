using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStatisticsService
    {
        Task<double> GetQuotesAverage();
        Task<double> GetQuotesDeviation();
        Task<double> GetQuotesMode();
        Task<double> GetQuotesMedian();
        Task<long> GetLostMessagesCount();
        void UpdateQuotes(int number);
    }
}