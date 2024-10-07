using System;
using System.IO;
using System.Text;

namespace Socks.IO
{
	public class StringStream : Stream
	{
		private readonly StringBuilder _content;
		private readonly Encoding _encoding;
		private int _position;
		private int _length;

		public StringStream(string content = "", Encoding encoding = null)
		{
			_content = new StringBuilder(content);
			_encoding = encoding ?? Encoding.UTF8;
		}

		public override bool CanRead => true;

		public override bool CanSeek => true;

		public override bool CanWrite => true;

		public override long Length => _length;

		public override long Position { get; set; }

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			var length = _content.Length;
			var position = offset + count;
			if (position > length)
			{
				return 0;
			}

			var charBuffer = new char[count];
			_content.CopyTo(_position, charBuffer, offset, count);
			charBuffer.CopyTo(buffer, 0);

			return _position += count;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
				case SeekOrigin.End:
					return _position = _length - (int)offset;
				case SeekOrigin.Current:
					return _position += (int)offset;
				case SeekOrigin.Begin:
				default:
					return _position = (int)offset;
			}
		}

		public override void SetLength(long value)
		{
			_length = (int)value;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			_content.Append(_encoding.GetString(buffer, offset, count));
		}

		public override string ToString()
		{
			return _content.ToString();
		}
	}
}
