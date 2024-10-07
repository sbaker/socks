using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Socks.Net
{
	public class SockRequest : SockRequest<StringContent>, IRequest
	{
		public SockRequest()
		{
		}

		public SockRequest(ISock sock) : base(sock)
		{
		}
	}

	public class SockRequest<TBody> : SockMessage<TBody>, IRequest where TBody : IBodyContent
	{
		public SockRequest()
		{
			Body = BodyContent.Null<TBody>();
		}

		public SockRequest(ISock sock) : base()
		{
			Body = BodyContent.Null<TBody>();
		}

		//public override byte[] GetContent(Encoding encoding)
		//{
		//	var read = 0;
		//	var buffer = new byte[Length];
		//	var receivable = Sock.GetStream();
		//	var received = 0;

		//	var segment = new byte[1024];
		//	var segments = new List<ReadOnlyMemory<byte>>();

		//	while ((read = receivable.Read(segment, 0, segment.Length)) > 0)
		//	{
		//		received += read;
		//		segments.Add(segment);
		//	}

		//	segments.ForEach(s => { });

		//	return buffer;
		//}
	}
}