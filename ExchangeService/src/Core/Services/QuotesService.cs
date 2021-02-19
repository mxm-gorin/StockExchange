using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Services
{
    public class QuotesService : IQuotesService
    {
        private readonly IQuotesRepository _quotesRepository;
        private readonly INotificationService _notificationService;

        public QuotesService(IQuotesRepository quotesRepository, INotificationService notificationService)
        {
            _quotesRepository = quotesRepository;
            _notificationService = notificationService;
        }
        
        public async Task Send()
        {
            var quotes = await _quotesRepository.Get();
            await _notificationService.Send(quotes);
        }
    }
}