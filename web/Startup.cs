using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Implementation.Services;
using Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using web.Demo;
using web.ioC;

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
            services.AddMvc();
            // using IOptions
            // services.Configure<SomeRootConfiguration>(_config.GetSection("SomeRoot"));

            // add configuration without IOptions POCO
            //var someRootConfiguration = new SomeRootConfiguration();
            //_config.GetSection("SomeRoot").Bind(someRootConfiguration);
            //services.AddSingleton(someRootConfiguration);

            services.ConfigurePOCO<SomeRootConfiguration>(_config.GetSection("SomeRoot"));
            services.ConfigurePOCO<DemoSecretsConfiguration>(_config.GetSection("DemoSecrets"));
            // default ioC
            // services.AddBusiness();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();                
            });
        }
    }
}
