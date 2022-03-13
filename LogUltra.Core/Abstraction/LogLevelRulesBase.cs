using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LogUltra.Core.Abstraction
{
    public abstract class LogLevelRulesBase
    {
        /// <summary>
        /// Log Levels Rules. Enable or Dissable a LogLevel
        /// </summary>
        public Dictionary<LogLevel, bool> LogLevelsRules { get; set; } = new Dictionary<LogLevel, bool>()
        {
            [LogLevel.Information] = true,
            [LogLevel.Warning] = true,
            [LogLevel.Error] = true,
            [LogLevel.Debug] = true,
            [LogLevel.Critical] = true,
            [LogLevel.Trace] = true
        };
    }
}
