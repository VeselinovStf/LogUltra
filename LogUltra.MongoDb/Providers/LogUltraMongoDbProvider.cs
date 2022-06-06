using LogUltra.Core.Abstraction.Format;
using LogUltra.MongoDb.Condigurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace LogUltra.MongoDb.Providers
{
    [ProviderAlias("LogUltraDb")]
    public class LogUltraMongoDbProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private LogUltraMongoDbConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, LogUltraMongoDbLogger> _loggers =
            new ConcurrentDictionary<string, LogUltraMongoDbLogger>();

        private readonly ITemplateFormatter _templateFormatter;
        private readonly ITemplateParser _templateParser;

        public LogUltraMongoDbProvider(
            ITemplateFormatter templateFormatter,
            ITemplateParser templateParser,
            IOptionsMonitor<LogUltraMongoDbConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);

            _templateFormatter = templateFormatter;
            _templateParser = templateParser;
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new LogUltraMongoDbLogger(_templateFormatter, _templateParser,name, GetCurrentConfig));

        private LogUltraMongoDbConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
