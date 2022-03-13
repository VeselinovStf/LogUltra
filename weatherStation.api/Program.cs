using LogUltra.Console.Extensions;
using LogUltra.Db.Condigurations;
using LogUltra.Db.Extensions;
using LogUltra.File.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;

namespace weatherStation.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().RegisterNLogWeb().GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging((whbc, logging) =>
            {
                logging.ClearProviders()
                    .AddLogUltraConsoleLogger(c =>
                    {
                        c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Trace] = true;
                    })
                    .AddLogUltraFileLogger(c =>
                    {
                        c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Trace] = true;
                        c.FilePath = @"LogUltra/log.txt";

                    })
                    .AddLogUltraDbLogger(c =>
                     {
                         c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Information] = true;
                         c.DbSettings = new LogUltraDatabaseSetting()
                         {
                             ConnectionString = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("ConnectionString").Value,
                             DatabaseName = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("DatabaseName").Value,
                             LogCollectionName = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("CollectionName").Value,
                             Source = "WeatherStation.API"
                         };
                     });
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .UseNLog();  // NLog: setup NLog for Dependency injection
    }
}
