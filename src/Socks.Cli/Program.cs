using System.Text;
using Socks;
using Socks.Client;
using Socks.Middleware;
using Socks.Net;
using Socks.Server;

async Task TestLocalConnection()
{
	// Setup the client connection
	var client = new SockFactory();
	client.ConfigurePipeline(builder =>
		builder.Use(() => new TestMiddleware("Client"))
	);

	// Setup the server to listen
	var server = new SockFactory();
	server.ConfigurePipeline(builder =>
		builder.Use(() => new TestMiddleware("Server"))
	);
	
	var listener = server.CreateListener(9000);

	var sock = await listener.AcceptAsync();

	var clientSock = client.ConnectAsync("127.0.0.1", 9000).Result;

	for (var i = 0; i < 10000; i++)
	{
		var sent = await clientSock.SendRequestAsync(new SockRequest { Body = $"Sent:{i}" });

		var buffer = new byte[1024];
		var received = await sock.AsReadable().ReadAsync(buffer);

        Console.WriteLine($"Received: {Encoding.Default.GetString(buffer)}");
	}

	listener.Dispose();
}

async Task<ISockServer> StartServer()
{
	var server = new SockServer();
	server.ConfigurePipeline(builder => {
		builder.Use(() => new TestMiddleware("server"))
		.Use(() => new DelegatingMiddleware((context, next) => {
			Console.WriteLine($"message received {context.Request.ReadAsStringAsync()}");
			return next(context);
		}));
	});

	server.StartAsync(9000).ContinueWith(async t =>
	{
	
	});

	return server;
}

async Task StartClient()
{
	try
	{
		var client = new SockClient();
		var sender = await client.CreateSender("sock://localhost:9000");

		string? text;
		while (!string.IsNullOrEmpty(text = Console.ReadLine()))
		{
			var response = await sender.WriteRequestAsync(new SockRequest { Body = text });

			var message = response.ReadAsStringAsync();
		}
	}
	catch(Exception e)
	{

	}
}

//await TestLocalConnection();

var server = await StartServer();

await StartClient();

await server.StopAsync();