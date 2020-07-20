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
    public class RequestTimingAdHocMiddelware
    {
        private readonly RequestDelegate _next;
        private int _requestCounter;

        public RequestTimingAdHocMiddelware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<RequestTimingAdHocMiddelware> logger)
        {
            var watch = Stopwatch.StartNew();
            await _next(context);
            watch.Stop();
            Interlocked.Increment(ref _requestCounter);
            logger.LogTrace("Request {requestNumbers} took  {requestTime} ms", _requestCounter, watch.ElapsedMilliseconds);
        }

    }
}
