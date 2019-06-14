using System.Threading.Tasks;

namespace Socks.Middleware
{
    internal class PipelineTerminationMiddleware : IMiddleware
    {
        public Task Invoke(ISockContext context)
        {
            return Task.CompletedTask;
        }

        void IMiddleware.SetNext(MiddlewareDelegate next)
        {
        }
    }
}