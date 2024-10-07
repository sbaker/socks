using System.Dynamic;
using System.Text;
using Socks.Net;

namespace Socks
{
    public class DefaultSockContext : ISockContext
    {
        public DefaultSockContext(ISock sock)
        {
            Sock = sock;
            Response = new DefaultSockResponse(this, sock.AsWritable());

            var request = new DefaultSockRequest(this, sock);
            Request = request;
            Properties = new ExpandoObject();
        }

        public ISock Sock { get; }

        public ISockRequest Request { get; }

        public ISockResponse Response { get; }

        public dynamic Properties { get; }
    }
}