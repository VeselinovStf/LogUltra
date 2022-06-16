using LogUltra.Core.Abstraction.Database;
using LogUltra.Core.Abstraction.Logger;

namespace LogUltra.MongoDb.Condigurations
{
    public class LogUltraMongoDbConfiguration : LogUltraBaseConfiguration, ILogDb
    {
        public ILogUltraDataSetting DbSettings { get; set; }
    }
}
