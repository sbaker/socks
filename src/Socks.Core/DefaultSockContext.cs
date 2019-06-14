using System.Dynamic;

namespace Socks
{
    internal class DefaultSockContext : ISockContext
    {
        public DefaultSockContext(byte[] buffer, ISockConnection connection)
        {
            Connection = connection;
            Response = new DefaultSockResponse(this);
            Request = new DefaultSockRequest(buffer, this);
            Properties = new ExpandoObject();
        }

        public ISockConnection Connection { get; }

        public ISockRequest Request { get; }

        public ISockResponse Response { get; }

        public dynamic Properties { get; }
    }
}