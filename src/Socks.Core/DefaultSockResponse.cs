using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Socks
{
    public class DefaultSockResponse : ISockResponse
    {
        public DefaultSockResponse(ISockContext context)
        {
            Context = context;
        }

        public ISockContext Context { get; }

        public async Task WriteAsync(string value)
        {
            await Context.Connection.SendAsync(Encoding.UTF8.GetBytes(value));
        }

        public async Task WriteAsync<T>(T value)
        {
            await WriteAsync(JsonConvert.SerializeObject(value));
        }
    }
}