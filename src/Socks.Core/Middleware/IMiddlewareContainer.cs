using System;

namespace Socks.Middleware
{
    internal interface IMiddlewareContainer
	{
		IMiddlewarePipeline Pipeline { get; }

		void ConfigurePipeline(Action<IMiddlewarePipelineBuilder> builder);

		void UsePipeline(IMiddlewarePipeline pipeline);
	}
}