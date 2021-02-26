using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Utilities.Filters.interfaces
{
    public interface ISortFilter
    {
        string PropertyName { get; set; }
        ESortOperator? Operator { get; set; }
    }
}
