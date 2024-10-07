using Newtonsoft.Json;
using Socks.Net;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Socks
{

	public class DefaultSockRequest : SockRequest<StreamContent>, ISockRequest
    {
        public DefaultSockRequest(ISockContext context, ISock sock) : base(sock)
        {
            Context = context;
            Body = new StreamContent
            {
                Content = sock.GetStream()
            };
		}

        public ISockContext Context { get; }

		public async Task<T> ReadAsAsync<T>()
        {
            var body = await ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
		}

		public Task<Stream> ReadAsStreamAsync()
        {
            return Task.FromResult(Body.Content);
        }
    }
}