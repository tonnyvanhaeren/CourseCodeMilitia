using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace web.Demo.Middleware
{
    public class RequestTimingFactoryMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimingFactoryMiddleware> _logger;
        private int _requestCounter;

        public RequestTimingFactoryMiddleware(ILogger<RequestTimingFactoryMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var watch = Stopwatch.StartNew();
            await next(context);
            watch.Stop();
            Interlocked.Increment(ref _requestCounter);
            _logger.LogTrace("Request {requestNumbers} took  {requestTime} ms", _requestCounter, watch.ElapsedMilliseconds);

        }
    }
}
