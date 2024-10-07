using Socks.IO;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socks.Net
{
    public class SockResponse : SockMessage, IResponse
    {
		private Sock _socket;

		public SockResponse(Sock socket, Encoding encoding = null)
		{
			_socket = socket;

			using (var stream = new StringStream(encoding: encoding))
			{
				socket.Stream.CopyTo(stream);

				Body = new StringContent
				{
					Content = stream.ToString()
				};
			}
		}

		public override byte[] GetContent(Encoding encoding)
		{
			var read = 0;
			var buffer = new byte[Length];
			var receivable = Sock.GetStream();
			var received = 0;

			var segment = new byte[1024];
			var segments = new List<ReadOnlyMemory<byte>>();

			while ((read = receivable.Read(segment, 0, segment.Length)) > 0)
			{
				received += read;
				segments.Add(segment);
			}

			return buffer;
		}

		//internal async Task ReadFromSock(Encoding encoding)
		//{
		//	var received = 0;
		//	var sb = new StringBuilder();
		//	var buffer = new byte[8192];
		//	var receivable = _socket.AsReadable();

		//	while ((received = await receivable.ReadAsync(buffer)) > 0)
		//	{
		//		sb.Append(encoding.GetString(buffer, 0, received));
		//	}

		//	Body = sb.ToString();
		//}
	}

	public class SockResponse<TBody> : SockMessage<TBody>, IResponse where TBody : IBodyContent
	{
	}

	public class NetworkSockResponse : SockMessage<StreamContent>, IResponse
	{
		public NetworkSockResponse(ISock sock, Encoding encoding = null)
		{
			Sock = sock;
			Encoding = encoding ?? Encoding.UTF8;
			Body = new StreamContent
			{
				Content = sock.GetStream()
			};
		}

		public ISock Sock { get; }

		public Encoding Encoding { get; }
	}
}