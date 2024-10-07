using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Socks.Net
{
	public interface ISockFactory
	{
		IListenerSock CreateListener(int port);

		Task<ISock> ConnectAsync(string address, int port);
	}
}
