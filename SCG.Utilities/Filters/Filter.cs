using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Utilities.Filters.interfaces;

namespace WebApi.Utilities.Filters
{
    public class Filter: IFilter
    {
        public string PropertyName { get; set; }
        public EFilterOperator? Operator { get; set; }
        public string Value { get; set; }

        public Filter(
            string propertyName,
            string value,
            EFilterOperator @operator = EFilterOperator.Contains)
            : this()
        {
            PropertyName = propertyName;
            Value = value;
            Operator = @operator;
        }

        private Filter() { }
    }

    public enum EFilterOperator
    {
        Contains,
        Equals,
        NotEquals,
        GreaterThan,
        LessThan,
        Between,
        EndWith,
        StartWith
    }

}
