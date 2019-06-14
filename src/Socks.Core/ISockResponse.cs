using System.Threading.Tasks;

namespace Socks
{
    public interface ISockResponse
    {
        ISockContext Context { get; }

        Task WriteAsync(string value);

        Task WriteAsync<T>(T value);
    }
}