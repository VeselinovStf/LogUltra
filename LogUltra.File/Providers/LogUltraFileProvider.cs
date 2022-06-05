using LogUltra.Core.Abstraction;
using LogUltra.File.Condigurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace LogUltra.File.Providers
{
    [ProviderAlias("LogUltraFile")]
    public class LogUltraFileProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private LogUltraFileConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, LogUltraFileLogger> _loggers =
            new ConcurrentDictionary<string, LogUltraFileLogger>();

        private readonly ITemplateFormatter _templateFormatter;
        private readonly ITemplateParser _templateParser;

        public LogUltraFileProvider(
            ITemplateFormatter templateFormatter,
            ITemplateParser templateParser,
            IOptionsMonitor<LogUltraFileConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);

            _templateFormatter = templateFormatter;
            _templateParser = templateParser;
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new LogUltraFileLogger(_templateFormatter, _templateParser, name, GetCurrentConfig));

        private LogUltraFileConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
