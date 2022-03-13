using LogUltra.Db.Condigurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace LogUltra.Db.Providers
{
    [ProviderAlias("LogUltraDb")]
    public class LogUltraDbProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private LogUltraDbConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, LogUltraDbLogger> _loggers =
            new ConcurrentDictionary<string, LogUltraDbLogger>();

        public LogUltraDbProvider(
            IOptionsMonitor<LogUltraDbConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new LogUltraDbLogger(name, GetCurrentConfig));

        private LogUltraDbConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
