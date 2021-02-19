using System;
using System.Threading.Tasks;
using ConsoleUI.Interfaces;
using Core.Interfaces;

namespace ConsoleUI.Commands
{
    public class PrintStatisticsCommand : ICommand
    {
        public string Key { get; } = "Enter";

        private readonly IStatisticsService _statisticsService;

        public PrintStatisticsCommand(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        
        public async Task ExecuteAsync()
        {
            Console.WriteLine();
            Console.WriteLine($"Packets lost: {await _statisticsService.GetLostMessagesCount()}");
            Console.WriteLine($"Quotes average: {await _statisticsService.GetQuotesAverage()}");
            Console.WriteLine($"Quotes standard deviation: {await _statisticsService.GetQuotesDeviation()}");
            Console.WriteLine($"Quotes mode: {await _statisticsService.GetQuotesMode()}");
            Console.WriteLine($"Quotes median: {await _statisticsService.GetQuotesMedian()}");
            Console.WriteLine();
        }
    }
}