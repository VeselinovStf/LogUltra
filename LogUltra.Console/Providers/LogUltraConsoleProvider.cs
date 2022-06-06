using LogUltra.Console.Condigurations;
using LogUltra.Core.Abstraction.Format;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace LogUltra.Console.Providers
{
    [ProviderAlias("LogUltraConsole")]
    public class LogUltraConsoleProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private LogUltraConsoleConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, LogUltraConsoleLogger> _loggers =
            new ConcurrentDictionary<string, LogUltraConsoleLogger>();

        private readonly ITemplateFormatter _templateFormatter;
        private readonly ITemplateParser _templateParser;

        public LogUltraConsoleProvider(
            ITemplateFormatter templateFormatter,
            ITemplateParser templateParser,
            IOptionsMonitor<LogUltraConsoleConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);

            _templateFormatter = templateFormatter;
            _templateParser = templateParser;
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new LogUltraConsoleLogger(_templateFormatter, _templateParser,name, GetCurrentConfig));

        private LogUltraConsoleConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
