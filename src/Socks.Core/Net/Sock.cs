using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socks.Net
{
	public abstract class Sock : ConnectedSock, ISock
	{
		protected Sock(Socket socket, ISockCache cache) : base(socket, cache)
		{
		}

		public abstract void Close();

		public IReadableSock AsReadable()
		{
			return new ReadableSock(this, Cache);
		}

		public IWritableSock AsWritable()
		{
			return new WritableSock(this, Cache);
		}

		public Stream GetStream()
		{
			return Stream;
		}

		public abstract Task<IResponse> SendRequestAsync(IRequest request);

		public abstract Task SendResponseAsync(IResponse response);
	}


	public class SocketSock : Sock, ISock
    {
        private readonly WritableSock _sender;
        private readonly ReadableSock _receiver;

        public SocketSock(Socket socket, ISockCache cache) : base(socket, cache)
		{
            _sender = new WritableSock(this, cache);
            _receiver = new ReadableSock(this, cache);
		}

		public override Task<IResponse> SendRequestAsync(IRequest request)
		{
			return _sender.WriteRequestAsync(request);
		}

		public override Task SendResponseAsync(IResponse response)
        {
            return _sender.WriteResponseAsync(response);
		}

		public override void Close()
		{
			Socket.Close();
		}
	}

	public class NetworkSock : Sock, ISock
	{
		public NetworkSock(Socket socket, ISockCache cache) : base(socket, cache)
		{
		}

		protected Encoding Encoding { get; } = Encoding.UTF8;

		public override void Close()
		{
			Stream.Close();
		}

		public override Task<IResponse> SendRequestAsync(IRequest request)
		{
			request.CopyTo(Stream, Encoding);
			var response = new NetworkSockResponse(this, Encoding);
			return Task.FromResult<IResponse>(response);
		}

		public override Task SendResponseAsync(IResponse response)
		{
			response.CopyTo(Stream, Encoding);
			return Task.CompletedTask;
		}
	}
}