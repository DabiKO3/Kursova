using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kursova
{

    internal class Filters
    {
        private BindingList<Filter> _filters;
        
        public BindingList<Filter> Data
        {
            get { return _filters; }
            set { _filters = value; }
        }

        public Filters()
        {
            _filters = new BindingList<Filter>();
        }

        public void AddFilter(Filter filter)
        {
            _filters.Add(filter);
        }

        public void RemoveFilter(Filter filter)
        {
            _filters.Remove(filter);
        }
        public void ClearFilters()
        {
            _filters.Clear();
        }

    }

    
}
