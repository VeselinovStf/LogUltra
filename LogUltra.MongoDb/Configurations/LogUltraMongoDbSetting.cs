using LogUltra.Core.Abstraction.Database;
using LogUltra.Core.Abstraction.Log;

namespace LogUltra.MongoDb.Condigurations
{
    public class LogUltraMongoDbSetting : ILogUltraDataSetting, ILogSource
    {
        public string LogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public string Source { get; set; }
    }
}
