using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Socks.Middleware
{
    public class MiddlewarePipeline : IMiddlewarePipeline
    {
        private readonly IMiddleware[] _pipeline;

        public MiddlewarePipeline(IEnumerable<IMiddleware> pipeline)
        {
            _pipeline = pipeline.ToArray();
        }

        public async Task Execute(ISockContext context)
        {
            for (int i = 0; i < _pipeline.Length - 1; i++)
            {
                await _pipeline[i].Invoke(context);
            }
        }
    }
}