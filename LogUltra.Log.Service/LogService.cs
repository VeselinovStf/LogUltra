using LogUltra.Core.Abstraction;
using LogUltra.Log.Service.Constants;
using LogUltra.Log.Service.DTOs;
using LogUltra.Log.Service.Exceptions;
using LogUltra.Log.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace LogUltra.Log.Service
{
    public class LogService : ILogService<GetLogsResponseModel>
    {
        private readonly ILogUltraRepository<LogUltra.Models.Log> logRepository;

        public LogService(ILogUltraRepository<LogUltra.Models.Log> logRepository)
        {
            this.logRepository = logRepository;
        }

        public async Task<GetLogsResponseModel> GetAsync(string sortColumn,
            string sortColumnDirection,
            string searchValue,
            string level,
            string source,
            string exception,
            int pageSize,
            int skip)
        {
            try
            {
                var logs = this.logRepository
                    .GetAll();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    logs = logs.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    logs = logs.Where(m => m.Id.ToLower().Contains(searchValue)
                                                || m.Description.ToLower().Contains(searchValue)
                                                || m.Source.ToLower().Contains(searchValue)
                                                || m.Exception.ToLower().Contains(searchValue)
                                                || m.CreatedAt.ToString("dd-MM-yyyy hh:mm:ss").ToLower().Contains(searchValue)
                                                || m.Level.ToLower().Contains(searchValue));
                }

                if (!string.IsNullOrWhiteSpace(level))
                {
                    logs = logs.Where(m => m.Level.ToLower().Contains(level.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(source))
                {
                    logs = logs.Where(m => m.Source.ToLower().Contains(source.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(exception))
                {
                    logs = logs.Where(m => m.IsException.ToString().ToLower().Contains(exception.ToLower()));
                }

                var data = await logs
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();

                return new GetLogsResponseModel()
                {
                    Message = ConstantMessages.LogServiceMessages.SuccessMessages.GetAllLogsAsyncSuccessMessage,
                    Success = true,
                    Logs = new List<LogDTO>(data.Select(l => new LogDTO()
                    {
                        CreatedAt = l.CreatedAt,
                        Description = l.Description,
                        Exception = l.Exception,
                        Id = l.Id,
                        IsException = l.IsException,
                        Level = l.Level,
                        Source = l.Source,
                    }))
                };
            }
            catch (Exception ex)
            {
                throw new LogServiceException(ex.Message, ex.InnerException);
            }
        }
    }
}
