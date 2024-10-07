using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Socks.Net
{
	public class ConnectedSock : Disposable, IDisconnectableSock
	{
		public ConnectedSock(Socket socket, ISockCache cache)
		{
			Cache = cache;
			Socket = socket;
			Stream = new NetworkStream(socket);
		}

		public ISockCache Cache { get; }

		public Socket Socket { get; }

		public NetworkStream Stream { get; }

		public async Task DisconnectAsync(bool reuse = true)
		{
			await Stream.Socket.DisconnectAsync(reuse);

			if (reuse)
			{
				await Cache.AddReusableSocketAsync(Socket);
			}
		}
	}
}