namespace LogUltra.Core.Abstraction.Logger
{
    public class LogUltraBaseConfiguration : LogLevelRulesBase, ILogEvent
    {
        public int EventId { get; set; }

        public string TemplatePath { get; set; } = "LogUltra/logultra";

        public bool UseTemplate { get; set; } = true;
    }
}
