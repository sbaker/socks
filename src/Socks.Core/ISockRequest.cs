using Socks.Net;
using System.IO;
using System.Threading.Tasks;

namespace Socks
{

	public interface ISockRequest : IRequest
    {
		Task<T> ReadAsAsync<T>();

        Task<Stream> ReadAsStreamAsync();
    }
}