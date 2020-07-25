using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Demo.Filters
{
    public class DemoActionFilter : IActionFilter
    {
        public ILogger<DemoActionFilter> _logger { get; }

        public DemoActionFilter(ILogger<DemoActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Before executing action {action} with arguments \"{@arguments}\" and model state \"{@modelState}\"",
                context.ActionDescriptor.DisplayName,
                context.ActionArguments,
                context.ModelState);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("After executing action {action}.", context.ActionDescriptor.DisplayName);
        }


    }
}
