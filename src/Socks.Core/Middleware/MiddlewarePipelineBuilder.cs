using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Socks.Middleware
{
    public class MiddlewarePipelineBuilder : IMiddlewarePipelineBuilder
    {
        private List<MiddlewareRegistration> _registrations = new List<MiddlewareRegistration>();

        public IMiddlewarePipelineBuilder Use<TMiddleware>() where TMiddleware : IMiddleware
        {
            return this;
        }

        public IMiddlewarePipeline Build()
        {
            throw new System.NotImplementedException();
        }

        private class MiddlewareRegistration
        {
            public Type RegisteredType { get; set; }
            
            public Func<object[], IMiddleware> Activator { get; } = null;
        }
    }

    public interface IMiddlewarePipelineBuilder
    {
        IMiddlewarePipelineBuilder Use<TMiddleware>() where TMiddleware : IMiddleware;
    }
}