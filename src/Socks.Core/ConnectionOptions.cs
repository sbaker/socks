using System.Net.Sockets;

namespace Socks
{
    public class ConnectionOptions
    {
        public SocketType SocketType { get; set; } = SocketType.Stream;

        public ProtocolType ProtocolType { get; set; } = ProtocolType.IP;
    }
}