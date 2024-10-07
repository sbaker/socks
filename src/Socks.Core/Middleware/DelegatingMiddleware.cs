using System;
using System.Threading.Tasks;

namespace Socks.Middleware
{
    public class DelegatingMiddleware : DefaultMiddleware
    {
        private readonly Func<ISockContext, MiddlewareDelegate, Task> _middleware;

        public DelegatingMiddleware(Func<ISockContext, MiddlewareDelegate, Task> middleware)
        {
            _middleware = middleware ?? throw new ArgumentNullException(nameof(middleware));
        }

        public override Task Invoke(ISockContext context)
        {
            return _middleware(context, (c) => Next(c));
        }
    }
}