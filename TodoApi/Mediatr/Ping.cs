using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;

namespace TodoApi.Mediatr
{
    public class Ping : IRequest<string> { }
    public class PingHandler : IRequestHandler<Ping, string>
    {
       
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult("d1");
        }
    }
    // optional to show what happens with multiple handlers
    public class Ping2Handler : IRequestHandler<Ping, string>
    {
       public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult("d2");

        }
    }
}
