using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova
{
    public enum ComparisonOperator
    {
        Equals,
        Contains,
        notEquals
    }

    public class Filter
    {
        public string Field { get; set; }
        
        public ComparisonOperator Operator { get; set; }
        public string Value { get; set; }
        public Filter() { }

        public Filter(string field, string value, ComparisonOperator comparisonOperator)
        {
            this.Field = field;
            this.Value = value;
            this.Operator = comparisonOperator;
        }
    }

}
