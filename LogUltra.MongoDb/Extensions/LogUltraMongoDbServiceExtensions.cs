using LogUltra.Core.Abstraction;
using LogUltra.MongoDb.Condigurations;
using LogUltra.MongoDb.Providers;
using LogUltra.TemplateParser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace LogUltra.MongoDb.Extensions
{
    public static class LogUltraMongoDbServiceExtensions
    {
        public static ILoggingBuilder AddLogUltraMongoDbLogger(
      this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
               ServiceDescriptor.Singleton<ILoggerProvider, LogUltraMongoDbProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <LogUltraMongoDbConfiguration, LogUltraMongoDbProvider>(builder.Services);

            builder.Services.AddSingleton<ITemplateFormatter, TemplateFormatter>();
            builder.Services.AddSingleton<ITemplateParser, TemplateParser.TemplateParser>();

            builder.Services.AddScoped<ILogUltraRepository<LogUltra.Models.Log>, LogUltraMongoDbRepository>();

            return builder;
        }

        /// <summary>
        /// Add Log Ultra Db Logger to App
        /// </summary>
        public static ILoggingBuilder AddLogUltraMongoDbLogger<T>(
            this ILoggingBuilder builder,
            Action<LogUltraMongoDbConfiguration> configure, IConfiguration Configuration) where T : class, ILogUltraDataSetting, new()
        {
            builder.AddLogUltraMongoDbLogger();
            builder.Services.Configure(configure);

            builder.Services.Configure<T>(
                Configuration.GetSection(typeof(T).Name));

            builder.Services.AddSingleton<ILogUltraDataSetting>(sp =>
                 sp.GetRequiredService<IOptions<T>>().Value);

            return builder;
        }

    }
}
