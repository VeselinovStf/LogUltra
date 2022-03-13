namespace LogUltra.UI.ViewModels
{
    public class LogViewModel
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }

        public string Level { get; set; }


        public string Source { get; set; }


        public string Description { get; set; }

        public bool IsException { get; set; }

        public string Exception { get; set; }
    }
}
