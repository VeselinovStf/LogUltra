using System.Linq;

namespace LogUltra.Core.Abstraction
{
    public interface ILogUltraRepository<T>
    {
        IQueryable<T> GetAll();
    }
}
