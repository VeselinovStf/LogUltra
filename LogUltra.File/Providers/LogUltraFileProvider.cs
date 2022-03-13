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

        public LogUltraFileProvider(
            IOptionsMonitor<LogUltraFileConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new LogUltraFileLogger(name, GetCurrentConfig));

        private LogUltraFileConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
