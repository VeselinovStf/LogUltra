namespace LogUltra.Log.API.DTOs
{
    public class GetLogsResponseDTO
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int RecordsFiltered { get; set; }

        public int RecordsTotal { get; set; }

        public List<LogResponseDTO> Data { get; set; }
    }
}
