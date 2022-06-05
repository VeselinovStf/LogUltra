using LogUltra.Core.Abstraction;
using LogUltra.Db.Condigurations;
using LogUltra.Db.Providers;
using LogUltra.TemplateParser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace LogUltra.Db.Extensions
{
    public static class LogUltraExtensions
    {
        public static ILoggingBuilder AddLogUltraDbLogger(
              this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
               ServiceDescriptor.Singleton<ILoggerProvider, LogUltraDbProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <LogUltraDbConfiguration, LogUltraDbProvider>(builder.Services);

            builder.Services.AddSingleton<ITemplateFormatter, TemplateFormatter>();
            builder.Services.AddSingleton<ITemplateParser, TemplateParser.TemplateParser>();

            return builder;
        }

        /// <summary>
        /// Add Log Ultra Db Logger to App
        /// </summary>
        public static ILoggingBuilder AddLogUltraDbLogger<T>(
            this ILoggingBuilder builder,
            Action<LogUltraDbConfiguration> configure, IConfiguration Configuration) where T : class, ILogUltraDataSetting, new()
        {
            builder.AddLogUltraDbLogger();
            builder.Services.Configure(configure);

            builder.Services.Configure<T>(
                Configuration.GetSection(typeof(T).Name));

           builder.Services.AddSingleton<ILogUltraDataSetting>(sp =>
                sp.GetRequiredService<IOptions<T>>().Value);

            return builder;
        }
    }
}
