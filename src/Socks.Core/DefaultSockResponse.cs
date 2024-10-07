using Newtonsoft.Json;
using Socks.Net;
using System.Text;
using System.Threading.Tasks;

namespace Socks
{

	public class DefaultSockResponse : ISockResponse
    {
        public DefaultSockResponse(ISockContext context, IWritableSock sock)
        {
            Context = context;
            Sock = sock;
        }

        public ISockContext Context { get; }

		public IWritableSock Sock { get; }

		public async Task WriteAsync(string value)
        {
            await Sock.WriteAsync(Encoding.UTF8.GetBytes(value));
        }

        public async Task WriteAsync<T>(T value)
        {
            await WriteAsync(JsonConvert.SerializeObject(value));
        }
    }
}