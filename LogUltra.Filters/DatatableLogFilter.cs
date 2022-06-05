using LogUltra.Core.Abstraction.Filters;
using LogUltra.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace LogUltra.Filters
{
    public class DatatableLogFilter 
    {
        public IList<IFilter<Log, string>> StringFilters { get; set; }
        public DatatableLogFilter(IList<IFilter<Log, string>> stringFilters)
        {
            StringFilters = stringFilters;
        }

        public IQueryable<Log> Filter(IQueryable<Log> logs)
        {
            foreach (var filter in StringFilters)
            {
                logs = filter.Filter(logs, "");
            }

            return logs;
        }

        public DatatableLogFilter(IQueryable<Log> logs,
            string sortColumn,
            string sortColumnDirection,
            string searchValue,
            string level,
            string source,
            string exception,
            int pageSize,
            int skip)
        {
            Logs = logs;
            SortColumn = sortColumn;
            SortColumnDirection = sortColumnDirection;
            SearchValue = searchValue;
            Level = level;
            Source = source;
            Exception = exception;
            PageSize = pageSize;
            Skip = skip;
        }

        private IQueryable<Log> Logs { get; set; }
        private string SortColumn { get; set; }
        private string SortColumnDirection { get; set; }
        private string SearchValue { get; set; }
        private string Level { get; set; }
        private string Source { get; set; }
        private string Exception { get; set; }
        private int PageSize { get; set; }
        private int Skip { get; set; }
        public int Count
        {
            get
            {
                return Logs.Count();
            }
        }

        public IList<Log> Filter()
        {


            SortByLevel();

            SortBySource();

            SortByException();

            return GetByPage();
        }

        public IList<Log> GetByPage()
        {
            return Logs.Skip(Skip).Take(PageSize).ToList();
        }

        public void SortByException()
        {
            if (!string.IsNullOrWhiteSpace(Exception))
            {
                Logs = Logs.Where(m => m.IsException.ToString().ToLower().Contains(Exception.ToLower()));
            }
        }

        public void SortBySource()
        {
            if (!string.IsNullOrWhiteSpace(Level))
            {
                Logs = Logs.Where(m => m.Level.ToLower().Contains(Level.ToLower()));
            }
        }

        public void SortByLevel()
        {
            if (!string.IsNullOrWhiteSpace(Source))
            {
                Logs = Logs.Where(m => m.Source.ToLower().Contains(Source.ToLower()));
            }
        }


    }
}
