using LogUltra.Core.Abstraction.Database;

namespace LogUltra.ExampleUse
{
    public class LoggingDatabaseSetting : ILogUltraDataSetting
    {
        public string LogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Source { get; set; }
    }
}
