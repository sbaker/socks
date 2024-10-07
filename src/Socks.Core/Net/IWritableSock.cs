using System.Threading.Tasks;

namespace Socks.Net
{
    public interface IWritableSock : IDisconnectableSock
    {
        Task<int> WriteAsync(byte[] buffer);

		Task<IResponse> WriteRequestAsync(IRequest request);

		Task WriteResponseAsync(IResponse response);
    }
}
