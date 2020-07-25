using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Demo.Filters
{
    public class DemoExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<DemoExceptionFilter> _logger ;

        public string Suffix { get; set; } = "by ServiceFilterAttribute";

        public DemoExceptionFilter(ILogger<DemoExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                _logger.LogInformation("Transforming ArgumentException in 400 {suffix}", Suffix);
                context.Result = new BadRequestResult();
            }
        }
    }
}
