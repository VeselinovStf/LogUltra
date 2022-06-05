using LogUltra.Core.Abstraction.Filters;
using LogUltra.Models;
using System.Linq;

namespace LogUltra.Filters
{
    internal class WhereByString : IFilter<Log, string>
    {
        public IQueryable<Log> Filter(IQueryable<Log> entity, string by)
        {
            if (!string.IsNullOrEmpty(by))
            {
                return entity.Where(m => m.Id.ToLower().Contains(by)
                                            || m.Description.ToLower().Contains(by)
                                            || m.Source.ToLower().Contains(by)
                                            || m.Exception.ToLower().Contains(by)
                                            || m.CreatedAt.ToString("dd-MM-yyyy hh:mm:ss").ToLower().Contains(by)
                                            || m.Level.ToLower().Contains(by));
            }

            return entity;
        }
    }
}
