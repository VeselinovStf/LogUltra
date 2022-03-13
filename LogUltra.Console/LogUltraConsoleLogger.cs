using LogUltra.Console.Condigurations;
using Microsoft.Extensions.Logging;
using System;

namespace LogUltra.Console
{
    public class LogUltraConsoleLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<LogUltraConsoleConfiguration> _getCurrentConfig;

        public LogUltraConsoleLogger(
            string name,
            Func<LogUltraConsoleConfiguration> getCurrentConfig) =>
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

            LogUltraConsoleConfiguration config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                var levelOption = config.LogLevelsRules.ContainsKey(logLevel);
                if (levelOption)
                {

                    if (config.LogLevelsRules[logLevel])
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
