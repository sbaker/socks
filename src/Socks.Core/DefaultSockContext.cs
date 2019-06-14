namespace Socks
{
    internal class DefaultSockContext : ISockContext
    {
        public DefaultSockContext(byte[] buffer, ISockConnection connection)
        {
            Connection = connection;
            Response = new DefaultSockResponse(this);
            Request = new DefaultSockRequest(buffer, this);
        }

        public ISockConnection Connection { get; }

        public ISockRequest Request { get; }

        public ISockResponse Response { get; }
    }
}