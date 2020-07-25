using Business.Implementation.Services;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Filters;

namespace web.ioC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRequiredMvcComponents(this IServiceCollection services)
        {
            services.AddTransient<ApiExceptionFilter>();

            var mvcBuilder = services.AddMvcCore(options =>
            {
                options.Filters.AddService<ApiExceptionFilter>();
            });

            mvcBuilder.AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.PropertyNamingPolicy = null;
               options.JsonSerializerOptions.DictionaryKeyPolicy = null;
           });

            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            return services;
        }

        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            // services.AddSingleton<IGroupsService, InMemoryGroupsService>();
            services.AddScoped<IGroupsService, GroupService>();

            // More Services ...

            return services;
        }

        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
