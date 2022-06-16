using System;

namespace LogUltra.Log.Service.DTOs
{
    public class LogDTO
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Level { get; set; }

        public string Source { get; set; }

        public string Description { get; set; }

        public bool IsException { get; set; }

        public string Exception { get; set; }
    }
}