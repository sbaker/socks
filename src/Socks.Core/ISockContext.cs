namespace Socks
{
    public interface ISockContext
    {
        dynamic Properties { get; }

        ISockConnection Connection { get; }

        ISockRequest Request { get; }

        ISockResponse Response { get; }
    }
}