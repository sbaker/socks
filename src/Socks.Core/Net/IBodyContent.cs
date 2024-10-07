using System.IO;

namespace Socks.Net
{
	public interface IBodyContent
	{
		long Length { get; }
	}

	public abstract class BodyContent : IBodyContent
	{
		public virtual long Length { get; }

		public static NullContent<TBodyContent> Null<TBodyContent>() where TBodyContent : IBodyContent
		{
			return default(TBodyContent);
		}
	}

	public class NullContent<TBody> : IBodyContent
	{
		public long Length => 0;

		public static implicit operator NullContent<TBody>(TBody body)
		{
			return body;
		}

		public static implicit operator TBody(NullContent<TBody> content)
		{
			return content;
		}
	}

	public class StreamContent : IBodyContent
	{
		public Stream Content { get; set; }

		public long Length => Content?.Length ?? 0;

		public static implicit operator StreamContent(Stream content)
		{
			return new StreamContent { Content = content };
		}

		public static implicit operator Stream(StreamContent content)
		{
			return content.Content;
		}
	}

	public class StringContent : IBodyContent
	{
		public string Content { get; set; }

		public long Length => Content?.Length ?? 0;

		public static implicit operator StringContent(string content)
		{
			return new StringContent { Content = content };
		}

		public static implicit operator string(StringContent content)
		{
			return content.Content;
		}
	}
}