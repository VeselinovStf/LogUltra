namespace LogUltra.UI.Models
{
    public class ListLogsRequestModel
    {
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }
        public string Level { get; set; }
        public string Source { get; set; }
        public string Exception { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
    }
}
