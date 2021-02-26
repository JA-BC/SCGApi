using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Utilities.Filters.interfaces
{
    public interface IFilter
    {
        string PropertyName { get; set; }
        EFilterOperator? Operator { get; set; }
        string Value { get; set; }
    }
}
