using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace LogUltra.UI.Tables
{
    public class BaseDataTableRequest
    {
        public BaseDataTableRequest(HttpRequest request)
        {
            this.Draw = request.Form["draw"].FirstOrDefault();
            this.Start = request.Form["start"].FirstOrDefault();
            this.Length = request.Form["length"].FirstOrDefault();
            this.SortColumn = request.Form["columns[" + request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            this.SortColumnDirection = request.Form["order[0][dir]"].FirstOrDefault();
            this.SearchValue = request.Form["search[value]"].FirstOrDefault();
            this.PageSize = Length != null ? Convert.ToInt32(Length) : 0;
            this.Skip = request.Form["start"].FirstOrDefault() != null ? Convert.ToInt32(Start) : 0;
            this.RecordsTotal = 0;
        }

        public string Draw { get; }
        public string Start { get; }
        public int Skip { get; }
        public string Length { get; }
        public string SortColumn { get; }
        public string SortColumnDirection { get; }
        public string SearchValue { get; }
        public int PageSize { get; }
        public int RecordsTotal { get; }
    }
}
