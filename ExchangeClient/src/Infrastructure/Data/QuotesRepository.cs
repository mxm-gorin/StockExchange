using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Data
{
    public class QuotesRepository : IQuotesRepository
    {
        private static readonly ConcurrentDictionary<int, Counter> Quotes = new ConcurrentDictionary<int, Counter>();
        private static readonly Counter QuotesTotalCount = new Counter();
        private static readonly Counter QuotesSum = new Counter();

        public void Add(int number)
        {
            if (Quotes.ContainsKey(number))
            {
                Quotes[number].Increment();
                QuotesSum.Add(ref number);
                QuotesTotalCount.Increment();
            }
            else
            {
                var added = Quotes.TryAdd(number, new Counter());
                if (added)
                {
                    QuotesSum.Add(ref number);
                    QuotesTotalCount.Increment();
                }
                else
                {
                    Add(number);
                }
            }
            
        }

        public double GetMean()
        {
            if (QuotesTotalCount.Value == 0) return 0;

            return (double) QuotesSum.Value / QuotesTotalCount.Value;
        }

        public Task<int> GetModeAsync()
        {
            return Task.Run(() =>
            {
                if (QuotesTotalCount.Value == 0) return 0;

                return Quotes.OrderByDescending(q => q.Value.Value).First().Key;
            });
        }

        public Task<double> GetDeviationAsync()
        {
            return Task.Run(() =>
            {
                if (QuotesTotalCount.Value == 0) return 0;

                var totalSum = QuotesSum.Value;
                var totalCount = QuotesTotalCount.Value;
                
                var mean = totalSum / totalCount;
                var poweredSum = Quotes.Sum(q => Math.Pow(q.Key - mean, 2) * q.Value.Value);

                return Math.Sqrt(poweredSum / totalCount);
            });
        }

        public Task<double> GetMedianAsync()
        {
            return Task.Run(() =>
            {
                if (QuotesTotalCount.Value == 0) return 0;

                var totalCount = QuotesTotalCount.Value;
                
                var roundedDownHalf = (int) totalCount / 2;
                long index = 0;
                var prevQuote = Quotes.First();
                var orderedQuotes = Quotes.OrderByDescending(q => q.Key);

                if (totalCount % 2 == 0)
                {
                    foreach (var quote in orderedQuotes)
                    {
                        index += quote.Value.Value;
                        if (index > roundedDownHalf) return (double) (prevQuote.Key + quote.Key) / 2;
                        prevQuote = quote;
                    }

                    return 0;
                }

                foreach (var quote in orderedQuotes)
                {
                    index += quote.Value.Value;
                    if (index == roundedDownHalf) return quote.Key;
                    if (index > roundedDownHalf) return prevQuote.Key;
                    prevQuote = quote;
                }

                return 0;
            });
        }
    }
}