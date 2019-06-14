using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Socks.Middleware;

namespace Socks
{
    public interface ISockConnection : IDisposable
    {
        Socket Socket { get; }

        void ConfigurePipeline(Action<IMiddlewarePipelineBuilder> builder);

        void UsePipeline(IMiddlewarePipeline pipeline);

        Task ConnectAsync(string address, int port);

        Task<ISockConnection> AcceptAsync();

        Task<int> SendAsync(byte[] buffer);

        Task<int> ReceiveAsync(byte[] buffer);

        void Listen(int port);
    }
}
