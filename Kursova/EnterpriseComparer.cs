using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova
{
  
    public class EnterpriseComparer : IComparer<Enterprise>
    {
        public int Compare(Enterprise x, Enterprise y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
