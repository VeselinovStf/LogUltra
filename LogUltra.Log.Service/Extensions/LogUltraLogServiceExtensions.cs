using LogUltra.Core.Abstraction.Database;
using LogUltra.Log.Service.Models;
using LogUltra.MongoDb;
using LogUltra.MongoDb.Condigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace LogUltra.Log.Service.Extensions
{
    public static class LogUltraLogServiceExtensions
    {
        /// <summary>
        /// Add Log Ultra Service Layer
        /// </summary>
        public static ILoggingBuilder AddLogUltraMongoDbService<T>(
            this ILoggingBuilder builder,
            Action<LogUltraMongoDbConfiguration> configure, IConfiguration Configuration) where T : class, ILogUltraDataSetting, new()
        {
            builder.Services.AddScoped<ILogService<GetLogsResponseModel>, LogService>();

            builder.Services.AddScoped<ILogUltraRepository<LogUltra.Models.Log>, LogUltraMongoDbRepository>();

            //builder.Services.AddLogUltraLogService();
            builder.Services.Configure(configure);

            builder.Services.Configure<T>(
                Configuration.GetSection(typeof(T).Name));

            builder.Services.AddSingleton<ILogUltraDataSetting>(sp =>
                 sp.GetRequiredService<IOptions<T>>().Value);

            return builder;
        }

    }
}
