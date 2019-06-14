using System;

namespace Socks.Middleware
{
    public interface IMiddlewarePipelineBuilder
    {
        IMiddlewarePipeline Build();

        IMiddlewarePipelineBuilder Use<TMiddleware>(Func<TMiddleware> activator) where TMiddleware : class, IMiddleware;
    }
}