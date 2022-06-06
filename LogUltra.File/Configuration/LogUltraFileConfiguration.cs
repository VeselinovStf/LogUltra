using LogUltra.Core.Abstraction.Log;

namespace LogUltra.File.Condigurations
{
    public class LogUltraFileConfiguration : LogUltraBaseConfiguration
    {
        /// <summary>
        /// File path to log file. Mendatory if UseFile is set to try.
        /// </summary>
        public string FilePath { get; set; }
    }
}