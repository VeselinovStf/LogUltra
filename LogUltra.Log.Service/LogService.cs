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

namespace LogUltra.Log.Service
{
    public class LogService : ILogService<GetLogsResponseModel>
    {
        private readonly ILogUltraRepository<LogUltra.Models.Log> logRepository;

        public LogService(ILogUltraRepository<LogUltra.Models.Log> logRepository)
        {
            this.logRepository = logRepository;
        }

        public async Task<GetLogsResponseModel> GetAsync()
        {
            try
            {
                var logs = await this.logRepository
                    .GetAll()
                    .ToListAsync();

                return new GetLogsResponseModel()
                {
                    Message = ConstantMessages.LogServiceMessages.SuccessMessages.GetAllLogsAsyncSuccessMessage,
                    Success = true,
                    Logs = new List<LogDTO>(logs.Select(l => new LogDTO()
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
