using LogUltra.Core.Abstraction;
using LogUltra.Log.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogUltra.Log.Service.Extensions
{
    public static class LogUltraLogServiceExtensions
    {
        /// <summary>
        /// Add Log Ultra File Logger to App
        /// </summary>
        public static void AddLogUltraLogService(
            this IServiceCollection services)
        {
            services.AddScoped<ILogService<GetLogsResponseModel>, LogService>();

        }

    }
}
