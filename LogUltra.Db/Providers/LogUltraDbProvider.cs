using LogUltra.Core.Abstraction;
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

        private readonly ITemplateFormatter _templateFormatter;
        private readonly ITemplateParser _templateParser;

        public LogUltraDbProvider(
            ITemplateFormatter templateFormatter,
            ITemplateParser templateParser,
            IOptionsMonitor<LogUltraDbConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);

            _templateFormatter = templateFormatter;
            _templateParser = templateParser;
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new LogUltraDbLogger(_templateFormatter, _templateParser,name, GetCurrentConfig));

        private LogUltraDbConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
