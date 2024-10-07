using Socks.Middleware;
using Socks.Net;
using Socks.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socks.Server
{
    public class SockServer : MiddlewareContainer, ISockServer
	{
		public SockServer()
		{
			SockFactory = new SockFactory();
			Serializer = new SockSerializer();
		}

		public ISockFactory SockFactory { get; }

		public ISockSerializer Serializer { get; }

		public bool Stopped { get; private set; }

		public async Task StartAsync(int port)
		{
			using (var listener = SockFactory.CreateListener(port))
			{
				while (!Stopped)
				{
					using (var accepted = await listener.AcceptAsync())
					{
						var context = new DefaultSockContext(accepted);
						await Pipeline.ExecuteAsync(context);
						accepted.Close();
					}
				}
			}
		}

		public Task StopAsync()
		{
			Stopped = true;
			Dispose();
			return Task.CompletedTask;
		}

		public void Dispose()
		{
		}
	}
}
