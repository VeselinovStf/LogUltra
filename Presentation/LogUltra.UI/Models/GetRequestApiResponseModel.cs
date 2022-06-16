using LogUltra.UI.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LogUltra.UI.Models
{
    public class GetRequestApiResponseModel
    {
        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty("message")]

        public string Message { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public List<LogViewModel> Data { get; set; }
    }
}
