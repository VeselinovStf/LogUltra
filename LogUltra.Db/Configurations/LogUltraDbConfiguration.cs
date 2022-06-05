using LogUltra.Core.Abstraction;

namespace LogUltra.Db.Condigurations
{
    public class LogUltraDbConfiguration : LogUltraBaseConfiguration, ILogDb
    {
        public ILogUltraDataSetting DbSettings { get; set; }
    }
}
