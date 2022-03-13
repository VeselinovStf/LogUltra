using LogUltra.Db.Condigurations;
using LogUltra.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace LogUltra.Db
{
    public class LogUltraDbLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<LogUltraDbConfiguration> _getCurrentConfig;

        public LogUltraDbLogger(
            string name,
            Func<LogUltraDbConfiguration> getCurrentConfig) =>
            (_name, _getCurrentConfig) = (name, getCurrentConfig);

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) =>
            _getCurrentConfig().LogLevelsRules.ContainsKey(logLevel);

        public void Log<TState>(
            Microsoft.Extensions.Logging.LogLevel logLevel,
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

            LogUltraDbConfiguration config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                var levelOption = config.LogLevelsRules.ContainsKey(logLevel);
                if (levelOption)
                {

                    if (config.LogLevelsRules[logLevel])
                    {
                        Task.Run(() =>
                        {
                            var client = new MongoClient(config.DbSettings.ConnectionString);
                            var database = client.GetDatabase(config.DbSettings.DatabaseName);

                            var logs = database.GetCollection<Log>(config.DbSettings.LogCollectionName);

                            logs.InsertOneAsync(new Log()
                            {
                                CreatedAt = DateTime.UtcNow,
                                Description = $"{eventId.Id}:{state}",
                                Exception = exception == null ? $"" : $"{exception}{Environment.NewLine}{exception.Message}{Environment.NewLine}{exception.StackTrace}{Environment.NewLine}{exception.InnerException}",
                                IsException = exception == null,
                                Level = logLevel.ToString(),
                                Source = config.DbSettings.Source
                            });
                            return Task.CompletedTask;
                        });

                    }

                }
            }
        }
    }
}
