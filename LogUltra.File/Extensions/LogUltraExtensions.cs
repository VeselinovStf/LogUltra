using LogUltra.File.Condigurations;
using LogUltra.File.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace LogUltra.File.Extensions
{
    public static class LogUltraExtensions
    {

        public static ILoggingBuilder AddLogUltraFileLogger(
               this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
               ServiceDescriptor.Singleton<ILoggerProvider, LogUltraFileProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <LogUltraFileConfiguration, LogUltraFileProvider>(builder.Services);

            return builder;
        }



        /// <summary>
        /// Add Log Ultra File Logger to App
        /// </summary>
        public static ILoggingBuilder AddLogUltraFileLogger(
            this ILoggingBuilder builder,
            Action<LogUltraFileConfiguration> configure)
        {
            builder.AddLogUltraFileLogger();
            builder.Services.Configure(configure);

            return builder;
        }


    }
}
