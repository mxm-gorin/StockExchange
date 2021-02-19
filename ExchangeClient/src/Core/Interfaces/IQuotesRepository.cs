using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IQuotesRepository
    {
        void Add(int number);
        double GetMean();
        Task<int> GetModeAsync();
        Task<double> GetMedianAsync();
        Task<double> GetDeviationAsync();
    }
}