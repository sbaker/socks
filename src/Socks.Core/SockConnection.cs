using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Socks.Middleware;

namespace Socks
{
    public class SockConnection : ISockConnection
    {
        public SockConnection(ConnectionOptions options = null)
        {
            options = options ?? new ConnectionOptions();
            Socket = new Socket(options.SocketType, options.ProtocolType);
        }

        internal SockConnection(Socket socket, IMiddlewarePipeline pipeline)
        {
            UsePipeline(pipeline);
            Socket = socket;
        }

        private bool IsDisposed { get; set; } = false;

        protected IMiddlewarePipeline Pipeline { get; private set; }

        public Socket Socket { get; }

        public void ConfigurePipeline(Action<IMiddlewarePipelineBuilder> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var pipeline = new MiddlewarePipelineBuilder();

            builder(pipeline);

            UsePipeline(pipeline.Build());
        }

        public void UsePipeline(IMiddlewarePipeline pipeline)
        {
            AssertNotDisposed();

            if (Pipeline != null)
            {
                throw new InvalidOperationException("Pipeline can only be set once.");
            }

            Pipeline = pipeline;
        }

        public async Task ConnectAsync(string address, int port)
        {
            AssertNotDisposed();

            await Socket.ConnectAsync(address, port);
        }

        public async Task<int> SendAsync(byte[] buffer)
        {
            AssertNotDisposed();

            return await Socket.SendAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
        }

        public async Task<ISockConnection> AcceptAsync(byte[] buffer)
        {
            var accept = await Socket.AcceptAsync();
            var connection = new SockConnection(accept, Pipeline);
            return connection;
        }

        public async Task<int> ReceiveAsync(byte[] buffer)
        {
            var received = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

            var context = new DefaultSockContext(buffer, this);

            await Pipeline.Execute(context);

            return received;
        }

        public void Listen(int port)
        {
            Socket.Bind(new IPEndPoint(IPAddress.Loopback, port));
            Socket.Listen(10);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            AssertNotDisposed();

            if (disposing)
            {
                Socket?.Dispose();
                IsDisposed = true;
            }
        }

        private void AssertNotDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(nameof(SockConnection));
            }
        }
    }
}