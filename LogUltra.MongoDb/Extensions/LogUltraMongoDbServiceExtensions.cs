using LogUltra.Core.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogUltra.MongoDb
{
    public static class LogUltraMongoDbServiceExtensions
    {
        /// <summary>
        /// Add Log Ultra File Logger to App
        /// </summary>
        public static void AddLogUltraMongoDb(
            this IServiceCollection services)
        {
            services.AddScoped<ILogUltraRepository<LogUltra.Models.Log>, LogUltraMongoDbRepository>();

        }

    }
}
