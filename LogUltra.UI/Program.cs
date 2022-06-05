using LogUltra.Console.Extensions;
using LogUltra.Db.Condigurations;
using LogUltra.Db.Extensions;
using LogUltra.File.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogUltra.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((appConfiguration) =>
                    {
                        appConfiguration.AddJsonFile("appsettings.json", optional: true);
                    });

                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureLogging((whbc, logging) =>
                     {
                         logging.ClearProviders()
                             .AddLogUltraConsoleLogger(c =>
                             {
                                 c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Trace] = true;
                                 c.TemplatePath = "LogUltra/logultra";
                                 c.UseTemplate = true;
                             })
                             .AddLogUltraFileLogger(c =>
                             {
                                 c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Trace] = true;
                                 c.FilePath = @"LogUltra/log.txt";
                                 c.TemplatePath = "LogUltra/logultra";
                                 c.UseTemplate = true;

                             })
                             .AddLogUltraDbLogger(c =>
                             {
                                 c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Information] = true;
                                 c.DbSettings = new LogUltraDatabaseSetting()
                                 {
                                     ConnectionString = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("ConnectionString").Value,
                                     DatabaseName = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("DatabaseName").Value,
                                     LogCollectionName = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("LogCollectionName").Value,
                                     Source = "LogUltra.UI"
                                 };
                             });
                         logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                     });
                });
    }
}
