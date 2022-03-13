using LogUltra.Core.Abstraction;

namespace LogUltra.Db.Condigurations
{
    public class LogUltraDbConfiguration : LogLevelRulesBase, ILogEvent, ILogDb
    {
        public int EventId { get; set; }

        public ILogUltraDataSetting DbSettings { get; set; }
    }
}
