using System;
using System.Text;
using System.Threading.Tasks;
using Socks.Middleware;

namespace Socks.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new MiddlewarePipelineBuilder()
                .Use(() => new TestMiddleware());

            var client = new SockConnection();
            client.UsePipeline(builder.Build());

            var server = new SockConnection();
            server.UsePipeline(builder.Build());
            server.Listen(9000);

            var bytes = new byte[1024];
            var connection = server.AcceptAsync(bytes);

            client.ConnectAsync("127.0.0.1", 9000).Wait();

            var read = string.Empty;
            while (!string.IsNullOrEmpty(read = Console.ReadLine()))
            {
                var sent = client.SendAsync(Encoding.Default.GetBytes(read)).Result;
                Console.WriteLine($"Sent:{sent}");
                var received = connection.Result.ReceiveAsync(new byte[sent]);
            }

            client.Dispose();
            server.Dispose();
            connection.Dispose();
        }

        public class TestMiddleware : DefaultMiddleware
        {
            public TestMiddleware()
            {
            }

            public override async Task Invoke(ISockContext context)
            {
                Console.WriteLine($"Buffer length: {context.Request.Length}");
                Console.WriteLine($"Buffer data:\n{context.Request.ReadAsString()}");
                await Next(context);
            }
        }
    }
}
