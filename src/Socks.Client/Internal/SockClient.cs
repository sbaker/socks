using Socks.Middleware;
using System;
using System.Collections.Generic;
using System.Text;

namespace Socks.Client.Internal
{
    internal class SockClient : SockConnection, ISockClient
    {
        public SockClient(ConnectionOptions options) : base(options)
        {
        }
    }
}
