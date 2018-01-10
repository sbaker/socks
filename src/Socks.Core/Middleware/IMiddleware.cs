using System.Threading.Tasks;

namespace Socks.Middleware
{
    public delegate Task MiddlewareDelegate(IMiddlewareContext context, MiddlewareDelegate next);
    
    public interface IMiddleware
    {
        Task Invoke(IMiddlewareContext context);
    }

    public interface IMiddlewareContext
    {
        
    }
}