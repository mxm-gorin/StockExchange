using System.Threading.Tasks;

namespace ConsoleUI.Interfaces
{
    public interface ICommand
    {
        string Key { get; }
        Task ExecuteAsync();
    }
}