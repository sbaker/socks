using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Socks
{
    public class DefaultSockRequest : ISockRequest
    {
        public DefaultSockRequest(byte[] buffer, ISockContext context)
        {
            Buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
            Context = context;
        }

        public ISockContext Context { get; }

        public int Length
        {
            get
            {
                return Buffer.Length;
            }
        }

        protected byte[] Buffer { get; }

        public T ReadAs<T>()
        {
            return JsonConvert.DeserializeObject<T>(ReadAsString());
        }

        public Stream ReadAsStream()
        {
            return new MemoryStream(Buffer);
        }

        public string ReadAsString()
        {
            return Encoding.UTF8.GetString(Buffer);
        }
    }
}