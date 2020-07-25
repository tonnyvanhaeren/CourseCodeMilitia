using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public static async Task Main(string[] args)
        {
            // ConfigureNLog();
            var host = CreateHostBuilder(args).Build();
            await host.EnsureDbUpdateAsync();
            host.Run();
        }

        //TODO: replace with Nlog.config


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureLogging(builder =>
                //{
                //    builder.ClearProviders();
                //    builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                //})
                //.UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget("coloredConsole")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${logger} ${level} ${message} ${exception}"
            };

            config.AddTarget(consoleTarget);

            //var fileTarget = new FileTarget("file")
            //{
            //    FileName = "${basedir}/file.log",
            //    Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception} ${ndlc}"
            //};

            //config.AddTarget(fileTarget);

            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Info, consoleTarget, "web.*");
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, consoleTarget);

            // config.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }

    }
}
