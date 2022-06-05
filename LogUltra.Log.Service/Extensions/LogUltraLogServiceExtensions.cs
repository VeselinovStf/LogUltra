using LogUltra.Core.Abstraction;
using LogUltra.Log.Service.Models;
using Microsoft.Extensions.DependencyInjection;

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
