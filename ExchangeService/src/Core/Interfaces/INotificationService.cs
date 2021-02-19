using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface INotificationService
    {
        Task Send(int number);
    }
}