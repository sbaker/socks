using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Socks.Net
{
	public class ReadableSock : ConnectedSock, IReadableSock
    {
        private readonly Sock _socket;

        public ReadableSock(Sock sock, ISockCache cache) : base(sock.Socket, cache)
        {
            _socket = sock;
        }

        public async Task<int> ReadAsync(byte[] buffer)
        {
            return await Stream.ReadAsync(new ArraySegment<byte>(buffer));
        }

        public Task<IRequest> ReadRequestAsync()
        {
            throw new NotImplementedException();
        }
    }
}