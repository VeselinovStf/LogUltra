using LogUltra.Core.Abstraction.Services;
using System.Threading.Tasks;

namespace LogUltra.Core.Abstraction.Database
{
    public interface ILogService<T> where T : ILogServiceBaseResponse
    {
        Task<T> GetAsync(string sortColumn,
            string sortColumnDirection,
            string searchValue,
            string level,
            string source,
            string exception,
            int pageSize,
            int skip);
    }
}
