using LogUltra.Core.Abstraction.Services;
using LogUltra.Log.Service.DTOs;
using System.Collections.Generic;

namespace LogUltra.Log.Service.Models
{
    public class GetLogsResponseModel : ILogServiceBaseResponse
    {
        public GetLogsResponseModel()
        {
            this.Logs = new List<LogDTO>();
        }

        public List<LogDTO> Logs { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
