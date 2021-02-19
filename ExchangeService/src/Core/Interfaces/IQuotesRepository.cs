using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IQuotesRepository
    {
        Task<int> Get();
    }
}