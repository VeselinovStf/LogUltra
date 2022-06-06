using System.Linq;

namespace LogUltra.Core.Abstraction.Database
{
    public interface ILogUltraRepository<T>
    {
        IQueryable<T> GetAll();
    }
}
