using System.Linq;
using ConsoleUI.Interfaces;
using Core.Interfaces;

namespace ConsoleUI.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly ICommand[] _availableCommands;

        public CommandFactory(IStatisticsService statisticsService)
        {
            _availableCommands = new ICommand[]
            {
                new PrintStatisticsCommand(statisticsService),
            };
        }
        
        public ICommand GetCommand(string key)
        {
            var command = _availableCommands
                .FirstOrDefault(c => c.Key == key) ?? new NotFoundCommand(key);

            return command;
        }
    }
}