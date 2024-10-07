using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socks.Net
{
	public class WritableSock : ConnectedSock, IWritableSock
    {
		private readonly ISock _sock;

		public WritableSock(Sock sock, ISockCache cache) : base(sock.Socket, cache)
        {
			_sock = sock;
		}

        public async Task<int> WriteAsync(byte[] buffer)
        {
			await Stream.WriteAsync(new ArraySegment<byte>(buffer));
			return buffer.Length;
        }

		public Task<IResponse> WriteRequestAsync(IRequest request)
		{
			request.CopyTo(Stream, Encoding.UTF8);
			var response = new NetworkSockResponse(_sock, Encoding.UTF8);
			return Task.FromResult<IResponse>(response);
		}

		public Task WriteResponseAsync(IResponse response)
        {
            throw new NotImplementedException();
        }
    }
}