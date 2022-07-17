# LogUltra

![simple representation](https://github.com/VeselinovStf/Entity-API/blob/main/repo/demo.png)

ASP .NET CORE 3.1 Logger/ Monitoring Application


## Tech Stack

- ASP .NET CORE 3.1
- Entity Framework Core 3.1
- MongoDb
- AutoMapper
- Docker

## Run App Demo
  
```
docker-compose --env-file .env up --build
```

## Structure

- Core
  - LogUltra.Core.Abstraction
    - Application core Abstractions
- Presentation
  - UseCase
    - LogUltra.ExampleUse
      - Example use of LogUltra Providers
  - LogUltra.Log.API
    - Simple API for Logs requests
  - LogUltra.UI
    - Simple Razor Page Web UI for displaying loggs
- Providers
  - LogUltra.Console
    - Log to Console Provider
  - LogUltra.File
    - Log to File Provider
  - LogUltra.MongoDb
    - Log to Db ( currently MongoDb ) Provider
- Utility
	- Database
    	- LogUltra.Log.Service
        	-  defines an application's available operations from the perspective of interfacing client layers.
	- Format
    	- LogUltra.TemplateParser
        	-  simple parser for logultra configuration files

## Use Case

```cs
// Program.cs
webBuilder.ConfigureLogging((whbc, logging) =>
                     {
                         logging.ClearProviders()
						 		// Use some of the LogUltra Logging Providers
                             .AddLogUltraConsoleLogger(c => // Add Log Ultra Console Provider
                             {
                                 c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Trace] = true;
                                 c.TemplatePath = "LogUltra/logultra";
                                 c.UseTemplate = true;
                             })
                             .AddLogUltraFileLogger(c =>   // Add Log Ultra File Provider
                             {
                                 c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Trace] = true;
                                 c.FilePath = @"LogUltra/log.txt";
                                 c.TemplatePath = "LogUltra/logultra";
                                 c.UseTemplate = true;

                             })
                             .AddLogUltraMongoDbLogger<LoggingDatabaseSetting>(c =>  // Add Log Ultra Database Provider ( MongoDb )
                             {
                                 c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Information] = true;
                                 c.DbSettings = new LogUltraMongoDbSetting()
                                 {
                                     ConnectionString = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("ConnectionString").Value,
                                     DatabaseName = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("DatabaseName").Value,
                                     LogCollectionName = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("LogCollectionName").Value,
                                     Source = "LogUltra.UI"
                                 };
                             }, whbc.Configuration);
                         logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                     });
```