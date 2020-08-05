using Business.Implementation.Services;
using Business.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
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

                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireClaim("scope", "GroupManagement")
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            mvcBuilder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            mvcBuilder.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddConfiguredAuth(this IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:5002";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "GroupManagement";
                });

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
