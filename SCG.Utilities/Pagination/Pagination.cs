using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Utilities.Paginations
{
    public class Pagination
    {
        public int Page { get; private set; }
        public int Limit { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPage { get; private set; }

        public Pagination(int page, int limit, int totalCount)
        {
            Page = page;
            Limit = limit;
            TotalCount = totalCount;
            TotalPage = (int)Math.Ceiling(totalCount / (double)limit);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (Page > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (Page < TotalPage);
            }
        }

    }
}
