using LogUltra.Models;
using LogUltra.Core.Abstraction.Filters;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace LogUltra.Filters
{
    public class OrderByString : IFilter<Log, string>
    {
        public IQueryable<Log> Filter(IQueryable<Log> entity,string by)
        {
            if (!(string.IsNullOrEmpty(by)))
            {
                return entity.OrderBy(by);
            }

            return entity;
        }
    }
}
