using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kursova
{
    internal class Enterprises
    {
        private BindingList<Enterprise> enterprises;

        public BindingList<Enterprise> Data
        {
            get { return enterprises; }
            set { enterprises = value; }
        }

        public Enterprises()
        {
            enterprises = new BindingList<Enterprise>();
        }

        public void AddEnterprise(Enterprise enterprise)
        {
            enterprises.Add(enterprise);
        }

        public void RemoveEnterprise(Enterprise enterprise)
        {
            enterprises.Remove(enterprise);
        }
        public void ClearEnterprise()
        {
            enterprises.Clear();
        }

        public List<Enterprise> FindEnterprises(string searchTerm, string category)
        {

            var filteredEnterprises = enterprises.Where(item =>
            {
                var property = typeof(Enterprise).GetProperty(category, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                    return false;

                var value = property.GetValue(item) as string;

                return !string.IsNullOrEmpty(value) && value.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0;
            }).ToList();

            return filteredEnterprises;
        }

    }
}
