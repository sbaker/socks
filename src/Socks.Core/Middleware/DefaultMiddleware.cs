using System;
using System.Threading.Tasks;

namespace Socks.Middleware
{
    public abstract class DefaultMiddleware : IMiddleware
    {
        protected DefaultMiddleware(MiddlewareDelegate next)
        {
            Next = next;
        }

        protected MiddlewareDelegate Next { get;  }

        public abstract Task Invoke(IMiddlewareContext context);
    }

    internal class DelegatingMiddleware : DefaultMiddleware
    {
        private readonly Func<IMiddlewareContext, MiddlewareDelegate, Task> _middleware;

        public DelegatingMiddleware(Func<IMiddlewareContext, MiddlewareDelegate, Task> middleware, MiddlewareDelegate next) : base(next)
        {
            _middleware = middleware ?? throw new ArgumentNullException(nameof(middleware));
        }

        public override async Task Invoke(IMiddlewareContext context)
        {
            await _middleware(context, Next);
        }
    }
}