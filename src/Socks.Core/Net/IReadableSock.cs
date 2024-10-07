using System.Threading.Tasks;

namespace Socks.Net
{
	public interface IReadableSock
    {
        Task<int> ReadAsync(byte[] buffer);

        Task<IRequest> ReadRequestAsync();
    }
}
