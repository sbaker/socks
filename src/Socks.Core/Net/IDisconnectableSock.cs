using System.Threading.Tasks;

namespace Socks.Net
{
	public interface IDisconnectableSock
	{
		Task DisconnectAsync(bool reuse = true);
	}
}