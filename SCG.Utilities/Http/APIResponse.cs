using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Utilities.Http
{
    public class APIResponse<TModel>
    {
        public List<TModel> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
