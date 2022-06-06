using LogUltra.Core.Abstraction.Log;

namespace LogUltra.Core.Abstraction.Database
{
    public interface ILogUltraDataSetting : ILogSource
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string LogCollectionName { get; set; }
    }
}