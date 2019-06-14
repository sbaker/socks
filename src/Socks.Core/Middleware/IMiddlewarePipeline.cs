using System.Threading.Tasks;

namespace Socks.Middleware
{
    public interface IMiddlewarePipeline
    {
        Task Execute(ISockContext context);
    }
}