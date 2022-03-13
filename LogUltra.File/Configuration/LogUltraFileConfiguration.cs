using LogUltra.Core.Abstraction;

namespace LogUltra.File.Condigurations
{
    public class LogUltraFileConfiguration : LogLevelRulesBase, ILogEvent
    {
        public int EventId { get; set; }
        /// <summary>
        /// File path to log file. Mendatory if UseFile is set to try.
        /// </summary>
        public string FilePath { get; set; }
    }
}