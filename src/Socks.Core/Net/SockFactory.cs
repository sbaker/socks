using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Socks.Middleware;

namespace Socks.Net
{
    public class SockFactory : MiddlewareContainer, ISockFactory
    {
		private static readonly ISockCache Cache = new SockCache(CreateSocket);

        public SockFactory(ConnectionOptions options = null)
		{
			Options = options ?? new ConnectionOptions();
		}

		public ConnectionOptions Options { get; }

		private static Socket CreateSocket(ConnectionOptions options)
		{
			return new Socket(options.SocketType, options.ProtocolType);
		}

		public async Task<ISock> ConnectAsync(string address, int port)
		{
			var socket = CreateSocket(Options);
            await socket.ConnectAsync(address, port);

            return Options.UseNetworkStream
				? new NetworkSock(socket, Cache)
				: new SocketSock(socket, Cache);
		}

		public IListenerSock CreateListener(int port)
		{
			var socket = CreateSocket(Options);
			var endpoint = new IPEndPoint(IPAddress.Loopback, port);
			socket.Bind(endpoint);
			socket.Listen(10);
			return new ListenerSock(socket, Cache, Options);
        }
	}
}