using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova
{
    [Serializable]
    public class Enterprise
    {
        public string Name { get; set; }
        public string Rozryad { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string TimeWork { get; set; }
        public string DaysWork { get; set; }
        public string Poslygu { get; set; }
        public string FormaVlasnosty { get; set; }
        public bool IsSayt { get; set; }

        public Enterprise() { }

        public Enterprise(string name, string rozryad, string address, string phone, string specialization, string timeWork, string daysWork, string poslygu, string formaVlasnosty, bool isSayt)
        {
            this.Name = name;
            this.Rozryad = rozryad;
            this.Address = address;
            this.Phone = phone;
            this.Specialization = specialization;
            this.TimeWork = timeWork;
            this.DaysWork = daysWork;
            this.Poslygu = poslygu;
            this.FormaVlasnosty = formaVlasnosty;
            this.IsSayt = isSayt;
        }

    }
}
