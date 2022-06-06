using LogUltra.Core.Abstraction.Format;
using Microsoft.Extensions.DependencyInjection;

namespace LogUltra.TemplateParser.Extensions
{
    public static class LogUltraTemplateParserExtensions
    {
        /// <summary>
        /// Add Log Ultra Db to AppTemplating
        /// </summary>
        public static void AddLogUltraTemplating(
            this IServiceCollection services)
        {
            services.AddSingleton<ITemplateFormatter, TemplateFormatter>();
            services.AddSingleton<ITemplateParser, TemplateParser>();

        }
    }
}
