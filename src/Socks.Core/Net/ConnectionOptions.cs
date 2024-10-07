using System.Net.Sockets;

namespace Socks.Net
{
    public class ConnectionOptions
    {
        public SocketType SocketType { get; set; } = SocketType.Stream;

        public ProtocolType ProtocolType { get; set; } = ProtocolType.IP;

        public bool UseNetworkStream { get; set; } = true;
    }
}