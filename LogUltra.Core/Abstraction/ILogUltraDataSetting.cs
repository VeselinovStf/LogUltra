namespace LogUltra.Core.Abstraction
{
    public interface ILogUltraDataSetting : ILogSource
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string LogCollectionName { get; set; }
    }
}