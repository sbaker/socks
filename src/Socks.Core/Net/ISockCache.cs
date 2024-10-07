using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System;

namespace Socks.Net
{
	public interface ISockCache
	{
		Task<Socket> GetReusableSocketAsync(ConnectionOptions options);

		Task AddReusableSocketAsync(Socket socket);
	}

	public class SockCache : ISockCache
	{
		private static readonly ConcurrentQueue<Socket> _reusableSockets = new ConcurrentQueue<Socket>();

		private readonly Func<ConnectionOptions, Socket> _socketFactory;

		public SockCache(Func<ConnectionOptions, Socket> socketFactory)
		{
			_socketFactory = socketFactory;
		}

		public Task AddReusableSocketAsync(Socket socket)
		{
			_reusableSockets.Enqueue(socket);
			return Task.CompletedTask;
		}

		public Task<Socket> GetReusableSocketAsync(ConnectionOptions options)
		{
			Socket socket = null;
			
			if (!_reusableSockets.TryDequeue(out socket))
			{
				socket = _socketFactory.Invoke(options);
			}

			return Task.FromResult(socket);
		}
	}
}