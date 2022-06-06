using Microsoft.AspNetCore.Http;
using System.Linq;

namespace LogUltra.UI.Tables
{
    public class ListLogsDataTableRequest : BaseDataTableRequest
    {
        public ListLogsDataTableRequest(HttpRequest request) : base(request)
        {
            this.Level = request.Form["level"].FirstOrDefault();
            this.Source = request.Form["source"].FirstOrDefault();
            this.Exception = request.Form["exception"].FirstOrDefault();
        }

        public string Level { get; set; }

        public string Source { get; set; }

        public string Exception { get; set; }
    }
}
