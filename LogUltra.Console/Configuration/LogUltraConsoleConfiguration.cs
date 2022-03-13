using LogUltra.Core.Abstraction;

namespace LogUltra.Console.Condigurations
{
    public class LogUltraConsoleConfiguration : LogLevelRulesBase, ILogEvent
    {
        public int EventId { get; set; }
    }
}
