using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;

namespace web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureNLog();
            CreateHostBuilder(args).Build().Run();
        }

        //TODO: replace with Nlog.config
        private static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget("coloredConsole")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };

            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("file")
            {
                FileName = "${basedir}/file.log",
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception} ${ndlc}"
            };

            config.AddTarget(fileTarget);

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, consoleTarget);
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, consoleTarget, "web.ioC.*");
            config.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
