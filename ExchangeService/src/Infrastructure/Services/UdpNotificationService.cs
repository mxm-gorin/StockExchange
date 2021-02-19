using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Configs;
using Core.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class UdpNotificationService : INotificationService, IDisposable
    {
        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _endPoint;
        private static long _messageNumber = 1;
        
        public UdpNotificationService(IOptions<UdpConfig> udpConfig)
        {
            _udpClient = new UdpClient();
            _endPoint = new IPEndPoint(IPAddress.Parse(udpConfig.Value.IpAddress), udpConfig.Value.Port);
        }
        
        public async Task Send(int number)
        {
            var message = new MessageModel(number, _messageNumber);
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            await _udpClient.SendAsync(bytes, bytes.Length, _endPoint);
            Interlocked.Increment(ref _messageNumber);
        }

        public void Dispose()
        {
            _udpClient?.Close();
        }
    }
}