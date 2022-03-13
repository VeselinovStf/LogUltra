using LogUltra.Core.Abstraction;

namespace LogUltra.Db.Condigurations
{
    public class LogUltraDatabaseSetting : ILogUltraDataSetting, ILogSource
    {
        public string LogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public string Source { get; set; }
    }
}
