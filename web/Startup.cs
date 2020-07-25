using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Implementation.Services;
using Business.Services;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using web.ioC;

[assembly: ApiController]
namespace web
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddRequiredMvcComponents();

            services.AddDbContext<GroupManagementDbContext>(options =>
            {
                options.UseNpgsql(_config.GetConnectionString("GroupManagementDbContext"));
                options.EnableSensitiveDataLogging();
            });

            // services.AddTransient<RequestTimingFactoryMiddleware>();
            // services.AddTransient<DemoExceptionFilter>();

            // default ioC
            services.AddBusiness();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseMiddleware<RequestTimingAdHocMiddelware>();
            //app.UseMiddleware<RequestTimingFactoryMiddleware>();

            //app.Map("/ping", builder =>
            //{
            //    builder.Run(async (context) => { await context.Response.WriteAsync("pong"); });
            //});

            //app.Use(async (context, next) =>
            //{
            //    context.Response.OnStarting(() =>
            //    {
            //        context.Response.Headers.Add("X-Powered-By", "ASP.NET Core: From 0 to overkill");
            //        return Task.CompletedTask;
            //    });

            //    await next.Invoke();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();                
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("nothing found to respond");
            });
        }
    }
}
