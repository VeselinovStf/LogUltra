using LogUltra.Core.Abstraction.Services;
using System.Threading.Tasks;

namespace LogUltra.Core.Abstraction
{
    public interface ILogService<T> where T : ILogServiceBaseResponse
    {
        Task<T> GetAsync();
    }
}
