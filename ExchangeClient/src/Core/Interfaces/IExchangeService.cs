using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IExchangeService
    {
        Task<int> ReceiveQuotesAsync();
    }
}