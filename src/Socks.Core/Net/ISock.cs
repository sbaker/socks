using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Socks.Net
{
	public interface ISock : IDisconnectableSock, IDisposable
    {
		ISockCache Cache { get; }

		Stream GetStream();

		IReadableSock AsReadable();

		IWritableSock AsWritable();

		Task<IResponse> SendRequestAsync(IRequest request);

		Task SendResponseAsync(IResponse response);

		void Close();
	}
}
