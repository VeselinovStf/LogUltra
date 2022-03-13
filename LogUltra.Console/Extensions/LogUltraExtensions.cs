using LogUltra.Console.Condigurations;
using LogUltra.Console.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace LogUltra.Console.Extensions
{
    public static class LogUltraExtensions
    {
        public static ILoggingBuilder AddLogUltraConsoleLogger(
                this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, LogUltraConsoleProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <LogUltraConsoleConfiguration, LogUltraConsoleProvider>(builder.Services);

            return builder;
        }



        /// <summary>
        /// Add Log Ultra Console Logger to App
        /// </summary>
        public static ILoggingBuilder AddLogUltraConsoleLogger(
            this ILoggingBuilder builder,
            Action<LogUltraConsoleConfiguration> configure)
        {
            builder.AddLogUltraConsoleLogger();
            builder.Services.Configure(configure);

            return builder;
        }


    }
}
