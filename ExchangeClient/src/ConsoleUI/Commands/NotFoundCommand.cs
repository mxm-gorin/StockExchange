using System.Threading.Tasks;
using ConsoleUI.Interfaces;

namespace ConsoleUI.Commands
{
    public class NotFoundCommand : ICommand
    {
        public string Key { get; set; }

        public NotFoundCommand(string commandKey)
        {
            Key = commandKey;
        }

        public Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }
    }
}