using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Socks.Net
{
	public interface IMessage
	{
		long Length { get; }

		byte[] GetContent(Encoding encoding);

		Task<byte[]> GetContentAsync(Encoding encoding);

		void CopyTo(Stream stream, Encoding encoding);

		Task CopyToAsync(Stream stream, Encoding encoding);

		Task<T> ReadAsAsync<T>(Encoding encoding = null);

		Task<string> ReadAsStringAsync(Encoding encoding = null);


	}
}