using LogUltra.Core.Abstraction.Format;
using LogUltra.File.Condigurations;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace LogUltra.File
{
    public class LogUltraFileLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<LogUltraFileConfiguration> _getCurrentConfig;

        private readonly ITemplateFormatter _templateFormatter;
        private readonly ITemplateParser _templateParser;

        public LogUltraFileLogger(
            ITemplateFormatter templateFormatter,
            ITemplateParser templateParser,
            string name,
            Func<LogUltraFileConfiguration> getCurrentConfig)
        {
            (_name, _getCurrentConfig) = (name, getCurrentConfig);

            _templateFormatter = templateFormatter;
            _templateParser = templateParser;
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) =>
            _getCurrentConfig().LogLevelsRules.ContainsKey(logLevel);

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState,
            Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            LogUltraFileConfiguration config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                var levelOption = config.LogLevelsRules.ContainsKey(logLevel);
                if (levelOption)
                {

                    if (config.LogLevelsRules[logLevel])
                    {
                        var logFileDirectory = Path.GetDirectoryName(config.FilePath);

                        if (!Directory.Exists(logFileDirectory))
                        {
                            Directory.CreateDirectory(logFileDirectory);
                        }

                        if (config.UseTemplate)
                        {
                            var templateContent = this._templateParser
                                                       .GetTemplate(config.TemplatePath);

                            var template = this._templateFormatter
                                .Parse(
                                    logLevel,
                                    eventId,
                                    state,
                                    exception,
                                    formatter,
                                    _name,
                                    templateContent);

                            System.IO.File.AppendAllText(config.FilePath, template);
                        }
                        else
                        {
                            System.Console.WriteLine("-------------------------------------");
                            System.Console.WriteLine($"[{eventId.Id}: {logLevel}]");
                            System.Console.WriteLine($"{_name}");
                            System.Console.WriteLine($"{formatter(state, exception)}");
                            System.Console.WriteLine("-------------------------------------");
                        }



                    }

                }
            }
        }
    }
}
