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

            services.AddConfiguredAuth();

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

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();                
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("No Middleware could handle the request");
            });
        }
    }
}
