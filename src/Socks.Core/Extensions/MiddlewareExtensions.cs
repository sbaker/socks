﻿using System;
using System.Threading.Tasks;
using Socks.Middleware;

namespace Socks.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IMiddlewarePipelineBuilder Use(this IMiddlewarePipelineBuilder builder, Func<ISockContext, MiddlewareDelegate, Task> middleware)
        {
            builder.Use<DelegatingMiddleware>(() => new DelegatingMiddleware(middleware));
            return builder;
        }
    }
}