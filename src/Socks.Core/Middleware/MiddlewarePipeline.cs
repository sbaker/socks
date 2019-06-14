using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Socks.Middleware
{
    public class MiddlewarePipeline : IMiddlewarePipeline
    {
        private readonly IMiddleware _start;

        public MiddlewarePipeline(IMiddleware startingMiddleware)
        {
            _start = startingMiddleware;
        }

        public async Task ExecuteAsync(ISockContext context)
        {
            await _start.Invoke(context);
        }
    }
}