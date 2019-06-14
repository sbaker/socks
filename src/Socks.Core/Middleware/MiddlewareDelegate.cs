using System.Threading.Tasks;

namespace Socks.Middleware
{
    public delegate Task MiddlewareDelegate(ISockContext context);
}