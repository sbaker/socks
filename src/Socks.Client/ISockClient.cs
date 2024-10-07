using System;
using System.Threading.Tasks;
using Socks.Net;

namespace Socks.Client
{
    public interface ISockClient
    {
		ISockFactory SockFactory { get; }

		Task<IWritableSock> CreateSender(string url);

		Task<IResponse> SendAsync(string url, IRequest request);
	}
}
