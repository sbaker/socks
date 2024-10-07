using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socks.Net
{
    public interface IListenerSock : IDisposable
	{
		Socket Listener { get; }

		Task<ISock> AcceptAsync();
	}

	public class ListenerSock : Disposable, IListenerSock
	{
		private readonly Socket _socket;
		private readonly ISockCache _cache;
		private readonly ConnectionOptions _options;

		public ListenerSock(Socket socket, ISockCache cache, ConnectionOptions options)
		{
			_socket = socket;
			_cache = cache;
			_options = options;

			Add(_socket);
		}

		public Socket Listener => _socket;

		public async Task<ISock> AcceptAsync()
		{
			var socket = await _cache.GetReusableSocketAsync(_options);
			var accept = await _socket.AcceptAsync(socket);
			return _options.UseNetworkStream
				? new NetworkSock(accept, _cache)
				: new SocketSock(accept, _cache);
		}
	}
}
