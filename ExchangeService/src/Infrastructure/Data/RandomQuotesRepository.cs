using System;
using System.Threading.Tasks;
using Core.Configs;
using Core.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data
{
    public class RandomQuotesRepository : IQuotesRepository
    {
        private readonly QuotesConfig _quotesConfig;
        private readonly Random _rnd;
        
        public RandomQuotesRepository(IOptions<QuotesConfig> quotesConfig)
        {
            _rnd = new Random();
            _quotesConfig = quotesConfig.Value;
        }

        public Task<int> Get()
        {
            return Task.FromResult(_rnd.Next(_quotesConfig.MinThreshold, _quotesConfig.MaxThreshold + 1));
        }
    }
}