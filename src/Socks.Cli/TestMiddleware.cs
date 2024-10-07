using System;
using System.Threading.Tasks;
using Socks;
using Socks.Middleware;

public class TestMiddleware : DefaultMiddleware
{
	private readonly string _name;

	public TestMiddleware(string name = "")
	{
		_name = name;
	}

	public override async Task Invoke(ISockContext context)
	{
		Console.WriteLine($"{_name} Buffer length: {context.Request.Length}");
		Console.WriteLine($"{_name} Buffer data:\n{context.Request.ReadAsStringAsync()}");
		await Next(context);
	}
}

