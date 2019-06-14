namespace Socks
{
    public interface ISockContext
    {
        ISockConnection Connection { get; }

        ISockRequest Request { get; }

        ISockResponse Response { get; }
    }
}