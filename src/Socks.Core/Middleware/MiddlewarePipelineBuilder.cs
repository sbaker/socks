using System;
using System.Collections.Generic;

namespace Socks.Middleware
{
    public class MiddlewarePipelineBuilder : IMiddlewarePipelineBuilder
    {
        private List<MiddlewareRegistration> _registrations = new List<MiddlewareRegistration>();

        public IMiddlewarePipelineBuilder Use<TMiddleware>(Func<TMiddleware> activator) where TMiddleware : class, IMiddleware
        {
            _registrations.Add(new MiddlewareRegistration
            {
                Activator = activator
            });
            return this;
        }

        public IMiddlewarePipeline Build()
        {
            var middleware = new List<IMiddleware>();
            IMiddleware previous = null;

            foreach (var registration in _registrations.ToArray())
            {
                var current = registration.Activator();

                middleware.Add(current);

                if (previous != null)
                {
                    previous.SetNext(current.Invoke);
                }

                previous = current;
            }

            var pipelineTermination = new PipelineTerminationMiddleware();

            middleware.Add(pipelineTermination);

            if (previous != null)
            {
                previous.SetNext(pipelineTermination.Invoke);
            }

            return new MiddlewarePipeline(middleware.ToArray());
        }

        private class MiddlewareRegistration
        {
            public Func<IMiddleware> Activator { get; set; } = null;
        }
    }
}