using System;
using System.Threading.Tasks;
using ConsoleUI.Interfaces;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace ConsoleUI
{
    public class Application
    {
        private readonly ILogger<Application> _logger;
        private readonly ICommandFactory _commandFactory;
        private readonly IJobService _jobService;
        private readonly IStatisticsService _statisticsService;
        private readonly IQuotesRepository _quotesRepository;

        public Application(ILogger<Application> logger,
            ICommandFactory commandFactory,
            IJobService jobService, IStatisticsService statisticsService,
            IQuotesRepository quotesRepository)
        {
            _logger = logger;
            _commandFactory = commandFactory;
            _jobService = jobService;
            _statisticsService = statisticsService;
            _quotesRepository = quotesRepository;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("ConsoleUI started");

            try
            {
                _jobService.StartJobs();
                var key = ConsoleKey.Applications;

                while (key != ConsoleKey.Escape)
                {
                    key = Console.ReadKey(true).Key;
                    var command = _commandFactory.GetCommand(key.ToString());
                    await command.ExecuteAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to run");
            }
            finally
            {
                _jobService.StopJobs();
                _logger.LogInformation("App is closed");
            }
        }
    }
}