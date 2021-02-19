using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Core.Configs;
using Core.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class ExchangeService : IExchangeService, IDisposable
    {
        private readonly ILogger<ExchangeService> _logger;
        private readonly ICacheService _cacheService;
        private readonly UdpClient _udpClient;
        private static long _biggestMessageNumber;

        public ExchangeService(ILogger<ExchangeService> logger,
            IOptions<UdpConfig> udpConfig,
            ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
            _udpClient = new UdpClient(udpConfig.Value.Port);
            _udpClient.JoinMulticastGroup(IPAddress.Parse(udpConfig.Value.IpAddress), 60);
        }

        public async Task<int> ReceiveQuotesAsync()
        {
            var data = await _udpClient.ReceiveAsync();
            var json = Encoding.UTF8.GetString(data.Buffer);
            var message = JsonConvert.DeserializeObject<MessageModel>(json);

            if (message == null) throw new Exception("Failed to parse message");
            
            CountMessages(message);

            return message.Quote;
        }

        private void CountMessages(MessageModel message)
        {
            if (_biggestMessageNumber == 0)
            {
                _biggestMessageNumber = message.MessageNumber;
            }
            else if (_biggestMessageNumber < message.MessageNumber)
            {
                var lostMessagesCount = message.MessageNumber - _biggestMessageNumber + 1;
                _cacheService.LostMessagesCount += lostMessagesCount;
                _biggestMessageNumber = message.MessageNumber;
            }
            else if (_biggestMessageNumber > message.MessageNumber)
            {
                _cacheService.LostMessagesCount =- 1;
            }
            else
            {
                throw new Exception("Duplicate message received ");
            }
        }

        public void Dispose()
        {
            _udpClient?.Close();
        }
    }
}