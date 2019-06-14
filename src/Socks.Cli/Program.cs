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
            // Setup the client connection
            var client = new SockConnection();
            client.ConfigurePipeline(builder =>
                builder.Use(() => new TestMiddleware())
            );

            // Setup the server to listen
            var server = new SockConnection();
            server.ConfigurePipeline(builder =>
                builder.Use(() => new TestMiddleware())
            );
            server.Listen(9000);

            var connection = server.AcceptAsync();

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
