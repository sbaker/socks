using System;
using System.Threading;

namespace Socks.Middleware
{
	public class MiddlewareContainer : IMiddlewareContainer
	{
		private IMiddlewarePipeline _pipeline;

		public IMiddlewarePipeline Pipeline => _pipeline;


		public void ConfigurePipeline(Action<IMiddlewarePipelineBuilder> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			var pipeline = new MiddlewarePipelineBuilder();

			builder(pipeline);

			UsePipeline(pipeline.Build());
		}

		public void UsePipeline(IMiddlewarePipeline pipeline)
		{
			Interlocked.Exchange(ref _pipeline, pipeline);
		}
	}
}
