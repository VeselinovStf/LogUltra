# LogUltra

![simple representation]()

ASP .NET CORE 3.1 Logger/ Monitoring Application


## Tech Stack

- ASP .NET CORE 3.1
- Entity Framework Core 3.1
- MongoDb
- AutoMapper

## Environment Set Up
  
```
docker-compose -f .\docker-compose-mongo.yml up -d --build
```

## Structure

- Core
- Presentation
- Providers
- Utility
	- Database
	- Format

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