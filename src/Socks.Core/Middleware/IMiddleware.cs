using System.Threading.Tasks;

namespace Socks.Middleware
{
    public interface IMiddleware
    {
        void SetNext(MiddlewareDelegate next);

        Task Invoke(ISockContext context);
    }
}