using LogUltra.Core.Abstraction;

namespace LogUltra.MongoDb.Condigurations
{
    public class LogUltraMongoDbConfiguration : LogUltraBaseConfiguration, ILogDb
    {
        public ILogUltraDataSetting DbSettings { get; set; }
    }
}
