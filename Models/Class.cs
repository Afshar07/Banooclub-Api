using System.Collections.Generic;
using System;

namespace BanooClub.Models
{
    public class PageModel<TData>
    {
        public List<TData> Items { get; }
        public int Page { get; }
        public int Size { get; }
        public int TotalCount { get; }
        public int TotalPage { get; }
        public PageModel(int page, int size, int totalCount, List<TData> items)
        {
            Page = page;
            Size = size;
            TotalCount = totalCount;
            TotalPage = (int)Math.Ceiling((decimal)totalCount / size);
            Items = items;
        }
    }
}
