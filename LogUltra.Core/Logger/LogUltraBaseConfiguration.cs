namespace LogUltra.Core.Abstraction.Logger
{
    public class LogUltraBaseConfiguration : LogLevelRulesBase, ILogEvent
    {
        public int EventId { get; set; }

        public string TemplatePath { get; set; }

        public bool UseTemplate { get; set; }
    }
}
