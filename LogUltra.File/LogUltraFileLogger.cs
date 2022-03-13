using LogUltra.File.Condigurations;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace LogUltra.File
{
    public class LogUltraFileLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<LogUltraFileConfiguration> _getCurrentConfig;

        public LogUltraFileLogger(
            string name,
            Func<LogUltraFileConfiguration> getCurrentConfig) =>
            (_name, _getCurrentConfig) = (name, getCurrentConfig);

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

                        var messageBuilder = new StringBuilder();
                        messageBuilder.AppendLine("-------------------------------------");
                        messageBuilder.AppendLine($"[{eventId.Id}: {logLevel}]");
                        messageBuilder.AppendLine($"{_name}");
                        messageBuilder.AppendLine($"{formatter(state, exception)}");
                        messageBuilder.AppendLine("-------------------------------------");

                        System.IO.File.AppendAllText(config.FilePath, messageBuilder.ToString());

                    }

                }
            }
        }
    }
}
