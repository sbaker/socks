using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Socks.Net
{
	public abstract class SockMessage<TBody> : IMessage where TBody : IBodyContent
	{
		public TBody Body { get; init; }

		public long Length => Body.Length;

		internal ISock Sock { get; }

		protected static Encoding GetEncodingOrDefault(Encoding encoding = null)
		{
			return encoding ?? Encoding.UTF8;
		}

		public virtual void CopyTo(Stream stream, Encoding encoding)
		{
			var body = GetContent(encoding);
			stream.Write(body, 0, body.Length);
		}

		public virtual Task CopyToAsync(Stream stream, Encoding encoding)
		{
			var body = GetContent(encoding);
			return stream.WriteAsync(body, 0, body.Length);
		}

		public virtual byte[] GetContent(Encoding encoding)
		{
			var read = 0;
			var buffer = new byte[Length];
			var receivable = Sock.GetStream();
			var received = 0;

			var segment = new byte[1024];
			var segments = new List<ReadOnlyMemory<byte>>();

			while ((read = receivable.Read(segment, 0, segment.Length)) > 0)
			{
				received += read;
				segments.Add(segment);
			}

			return buffer;
		}

		public virtual Task<byte[]> GetContentAsync(Encoding encoding)
		{
			return Task.FromResult(GetContent(encoding));
		}

		public async Task<string> ReadAsStringAsync(Encoding encoding = null)
		{
			encoding = GetEncodingOrDefault(encoding);

			var received = 0;
			var sb = new StringBuilder();
			var buffer = new byte[8192];
			var receivable = Sock.AsReadable();

			while ((received = await receivable.ReadAsync(buffer)) > 0)
			{
				sb.Append(encoding.GetString(buffer, 0, received));
			}

			return sb.ToString();
		}

		public async Task<T> ReadAsAsync<T>(Encoding encoding = null)
		{
			var body = await ReadAsStringAsync(encoding);
			return JsonSerializer.Deserialize<T>(body);
		}
	}

	public abstract class SockMessage : SockMessage<StringContent>
	{
	}
}