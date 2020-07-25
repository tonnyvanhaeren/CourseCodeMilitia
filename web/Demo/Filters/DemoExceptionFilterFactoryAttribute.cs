using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Demo.Filters
{
    public class DemoExceptionFilterFactoryAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable { get; } = false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetRequiredService<DemoExceptionFilter>();
            filter.Suffix = $"by {nameof(DemoExceptionFilterFactoryAttribute)}";
            return filter;
        }
    }
}
