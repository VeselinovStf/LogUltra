﻿using LogUltra.Db.Condigurations;
using LogUltra.Db.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
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

            return builder;
        }

        /// <summary>
        /// Add Log Ultra Db Logger to App
        /// </summary>
        public static ILoggingBuilder AddLogUltraDbLogger(
            this ILoggingBuilder builder,
            Action<LogUltraDbConfiguration> configure)
        {
            builder.AddLogUltraDbLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}