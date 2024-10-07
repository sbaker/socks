using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Socks.Middleware;
using Socks.Net;
using Socks.Serialization;

namespace Socks.Server
{
    public interface ISockServer : IDisposable
    {
        ISockFactory SockFactory { get; }

        ISockSerializer Serializer { get; }

		bool Stopped { get; }

		Task StartAsync(int port);

        Task StopAsync();
    }
}
