using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova
{
    public abstract class DataServis
    {
 
        protected string name;
        protected string address;
        protected string phone;
        protected string specialization;
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
        }

        public DataServis(string name, string address, string phone, string specialization, 
            string formaVlasnosty)
        {
            this.name = name;
            this.address = address;
            this.phone = phone;
            this.specialization = specialization;
            this.formaVlasnosty = formaVlasnosty;
        }

      
        public virtual void DisplayInfo()
        {
            MessageBox.Show($"Назва: {Name}, Адрес: {Address},Телефон: {Phone},Спеціалізація: {Specialization},Форма властності: {FormaVlasnosty}.", "Інформація");
        }

        public virtual void Clear()
        {
            this.name = "";
            this.address = "";
            this.phone = "";
            this.specialization = "";
            this.formaVlasnosty = "";
        }

    }

}
