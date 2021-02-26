using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Utilities.Filters.interfaces;

namespace WebApi.Utilities.Filters
{
    public class SortFilter: ISortFilter
    {
        public string PropertyName { get; set; }
        public ESortOperator? Operator { get; set; }

        public SortFilter(string propertyName, ESortOperator @operator = ESortOperator.Ascendent)
            : this()
        {
            PropertyName = propertyName;
            Operator = @operator;
        }

        private SortFilter() { }
    }

    public enum ESortOperator
    {
        Ascendent,
        Descendent
    }
}
