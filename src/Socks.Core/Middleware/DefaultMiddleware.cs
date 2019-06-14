using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Socks.Middleware
{
    public abstract class DefaultMiddleware : IMiddleware
    {
        protected DefaultMiddleware()
        {
        }

        public MiddlewareDelegate Next { get; set; }

        public abstract Task Invoke(ISockContext context);

        void IMiddleware.SetNext(MiddlewareDelegate next) => Next = next;
    }

    public abstract class PayloadSerializerMiddleware : DefaultMiddleware
    {
        public PayloadSerializerMiddleware(Encoding encoding)
        {
            Encoding = encoding;
        }

        public Encoding Encoding { get; }
    }

    //public class JsonSerializerMiddleware : PayloadSerializerMiddleware
    //{
    //    public JsonSerializerMiddleware(Encoding encoding) : base(encoding)
    //    {
    //    }

    //    public override Task Invoke(IMiddlewareContext context)
    //    {
    //        JsonConvert.
    //    }
    //}
}