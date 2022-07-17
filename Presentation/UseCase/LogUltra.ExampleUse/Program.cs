using LogUltra.Console.Extensions;
using LogUltra.File.Extensions;
using LogUltra.MongoDb.Condigurations;
using LogUltra.MongoDb.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace LogUltra.ExampleUse
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
                            .AddLogUltraConsoleLogger()
                            .AddLogUltraFileLogger(c =>
                           {
                               c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Trace] = false;
                           })
                            .AddLogUltraMongoDbLogger<LoggingDatabaseSetting>(c =>
                           {
                               c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Trace] = false;
                               c.DbSettings = new LogUltraMongoDbSetting()
                               {
                                   ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"),
                                   DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME"),
                                   LogCollectionName = Environment.GetEnvironmentVariable("LOG_COLLECTION_NAME"),
                                   Source = Environment.GetEnvironmentVariable("EXAMPLE_DB_SOURCE_PROPERTY")
                               };
                           }, whbc.Configuration);
                      logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                  });
              });
    }
}

