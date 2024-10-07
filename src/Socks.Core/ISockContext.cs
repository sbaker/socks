using Socks.Net;

namespace Socks
{
    public interface ISockContext
    {
        dynamic Properties { get; }

        ISock Sock { get; }

        ISockRequest Request { get; }

        ISockResponse Response { get; }
    }
}