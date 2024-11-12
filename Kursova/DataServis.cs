using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova
{

    public abstract class DataServis
    {

        protected string name;
        protected string address;
        protected string phone;
        protected string specialization;
        protected string rozryad;
        protected string timeWork;
        protected string daysWork;
        protected string poslygu;
        protected string formaVlasnosty;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }
        public string Rozryad
        {
            get { return rozryad; }
            set { rozryad = value; }
        }
        public string TimeWork
        {
            get { return timeWork; }
            set { timeWork = value; }
        }
        public string DaysWork
        {
            get { return daysWork; }
            set { daysWork = value; }
        }
        public string Poslygu
        {
            get { return poslygu; }
            set { poslygu = value; }
        }
        public string FormaVlasnosty
        {
            get { return formaVlasnosty; }
            set { formaVlasnosty = value; }
        }

        public DataServis() 
        {
            this.name = "";
            this.address = "";
            this.phone = "";
            this.specialization = "";
            this.formaVlasnosty = "";
            this.formaVlasnosty = "";
            this.rozryad = "";
            this.timeWork = "";
            this.daysWork = "";
        }

        public DataServis(string name, string address, string phone, string specialization, string formaVlasnosty, string poslygu, string daysWork, string timeWork, string rozryad)
        {
            this.name = name;
            this.address = address;
            this.phone = phone;
            this.specialization = specialization;
            this.formaVlasnosty = formaVlasnosty;
            this.formaVlasnosty = poslygu;
            this.rozryad = rozryad;
            this.timeWork = timeWork;
            this.daysWork = daysWork;
        }

        public virtual void DisplayInfo()
        {

        }
    }

    public class Service : DataServis, IComparable<Service>
    {
        public int CompareTo(Service other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public Service() : base() { }

        public Service(string name, string address, string phone, string specialization, string formaVlasnosty, string poslygu, string daysWork, string timeWork, string rozryad)
            : base(name, address, phone, specialization, formaVlasnosty, poslygu, daysWork, timeWork, rozryad)
        {
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
        }
    }

    public class ServiceComparer : IComparer<Service>
    {
        public int Compare(Service x, Service y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

}
