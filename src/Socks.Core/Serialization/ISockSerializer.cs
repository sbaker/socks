using System;
using System.Collections.Generic;
using System.Text;

namespace Socks.Serialization
{
	public interface ISockSerializer
	{
		byte[] Serialize<T>(T value);

		T Deserialize<T>(byte[] data);
	}

	public class SockSerializer : ISockSerializer
	{
		public T Deserialize<T>(byte[] data)
		{
			throw new NotImplementedException();
		}

		public byte[] Serialize<T>(T value)
		{
			throw new NotImplementedException();
		}
	}
}
