using Business.Implementation.Services;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.ioC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IGroupsService, InMemoryGroupsService>();
            // More Services ...

            return services;
        }
    }
}
