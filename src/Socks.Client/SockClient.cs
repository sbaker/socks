using Socks.Middleware;
using Socks.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Socks.Client
{
    public class SockClient : ISockClient
    {
        public SockClient()
        {
            SockFactory = new SockFactory();
        }

        public ISockFactory SockFactory { get; }

        public Encoding Encoding { get; set; }

		public async Task<IWritableSock> CreateSender(string url)
		{
			var uri = new Uri(url);
            var socket = await SockFactory.ConnectAsync(uri.Host, uri.Port);
            return socket.AsWritable();
		}

		public async Task<IResponse> SendAsync(string url, IRequest request)
        {
            try
            {
                var sock = await CreateSender(url);
                var response = await sock.WriteRequestAsync(request);
				await sock.DisconnectAsync(true);
                return response;
			}
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
