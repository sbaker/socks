using System;
using System.Net.Sockets;
using Socks.Middleware;

namespace Socks
{
    public interface ISockConnection : IDisposable
    {
        Socket Socket { get; }

        void ConfigurePipeline(Action<IMiddlewarePipelineBuilder> builder);

        void UsePipeline(IMiddlewarePipeline pipeline);
    }

    public interface IMiddlewarePipeline
    {
        void Execute();
    }

    public class ConnectionOptions
    {
        public SocketType SocketType { get; set; } = SocketType.Rdm;

        public ProtocolType ProtocolType { get; set; } = ProtocolType.IP;
    }

    public abstract class SockConnection : ISockConnection
    {
        protected SockConnection(ConnectionOptions options)
        {
            Socket = new Socket(options.SocketType, options.ProtocolType);
        }

        protected bool IsDisposed { get; set; } = false;

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

        public void ConfigurePipeline()
        {
            throw new NotImplementedException();
        }

        public abstract void UsePipeline(IMiddlewarePipeline pipeline);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Socket?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
