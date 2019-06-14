using Socks.Middleware;
using System;
using System.Collections.Generic;
using System.Text;

namespace Socks.Server.Internal
{
    internal class SockServer : SockConnection, ISockServer
    {
        public SockServer(ConnectionOptions options) : base(options)
        {
        }

        //public override void UsePipeline(IMiddlewarePipeline pipeline)
        //{
            
        //}
    }
}
