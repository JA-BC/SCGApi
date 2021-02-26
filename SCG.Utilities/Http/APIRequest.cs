using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Utilities.Filters;
using WebApi.Utilities.Paginations;

namespace WebApi.Utilities.Http
{
    public class APIRequest
    {
        public Pagination Pagination { get; set; }
        public Filter[] Filters { get; set; }
        public SortFilter[] Sorts { get; set; }
    }
}
