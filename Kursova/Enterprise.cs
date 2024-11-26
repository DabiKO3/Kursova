using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Kursova
{
                   
   
    public interface IDetails
    {
        void DisplayDetails();
    }

    [Serializable]
    public class Enterprise : DataServis , IDetails
    {
     
        private int rozryad;
        private double qualitSrvices;
        private string poslygu;

        public string TimeWork { get; set; }
        public string DaysWork { get; set; }
        
    
        public int Rozryad
        {
            get { return rozryad; }
            set { rozryad = value; }
        }
        public string Poslygu
        {
            get { return poslygu; }
            set { poslygu = value; }
        }
        
        public double QualitSrvices
        {
            get { return qualitSrvices; }
            set { qualitSrvices = value; }
        }
        
        public bool IsSayt { get; set; }

    
        public Enterprise() : this("Добро", 0, "", "", "", "09:00-18:00", "Пн-Пт", "", "", false, 0) { }


        public Enterprise(string name, int rozryad, string address, string phone,
            string specialization, string timeWork, string daysWork, string poslygu,
            string formaVlasnosty, bool isSayt, double qualitSrvices)
            : base(name, address, phone, specialization, formaVlasnosty) 
        {
 

            this.Rozryad = rozryad;
            this.TimeWork = timeWork;
            this.DaysWork = daysWork;
            this.Poslygu = poslygu;
            this.Rozryad = rozryad;
            this.QualitSrvices = qualitSrvices;
            this.IsSayt = isSayt;
        }

      
        public override void Clear()
        {
            base.Clear();
                this.Rozryad = 0;
                this.TimeWork = "";
                this.DaysWork = "";
                this.Poslygu = "";
                this.IsSayt = false;
                this.QualitSrvices = 0;
        }

     
        public override void DisplayInfo()
        {
          
            base.DisplayInfo();
            MessageBox.Show($"Розряд: {Rozryad}\nЧас роботи: {TimeWork}\nДні роботи: {DaysWork}\nПослуга: {Poslygu}\nСайт: {IsSayt}.", "Інформація");
        }

    
        public bool IsValidPhoneNumber()
        {
            return Phone.Length == 10 && Phone.All(char.IsDigit);
        }

   
        public int CompareTo(Enterprise other)
        {
      
            return string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public string GetEnterpriseDetails()
        {
            return $"Назва: {Name}\nРозряд: {Rozryad}\nАдреса: {Address}\nТелефон: {Phone}\nСпеціалізація: {Specialization}\nЧас роботи: {TimeWork}\nДні роботи: {DaysWork}\nПослуга: {Poslygu}\nФорма власності: {FormaVlasnosty}\nСайт: {IsSayt}";
        }
   
        public void DisplayDetails()
        {
            MessageBox.Show($"Назва: {Name}\nАдрес: {Address}\nТелефон: {Phone}\nСпеціалізація: {Specialization}\nФорма властності: {FormaVlasnosty}\nРозряд: {Rozryad}\nЧас роботи: {TimeWork}\nДні роботи: {DaysWork}\nПослуга: {Poslygu}\nСайт: {IsSayt}.", "Інформація"); ;
        }

        public static void DisplayInfo(string name)
        {
            MessageBox.Show($"Підрпиємство: {name}");
        }


        public static void DisplayInfo(Enterprise ent)
        {
            MessageBox.Show($"Назва: {ent.Name}\nАдрес: {ent.Address}\nТелефон: {ent.Phone}\nСпеціалізація: {ent.Specialization}\nФорма властності: {ent.FormaVlasnosty}\nРозряд: {ent.Rozryad}\nЧас роботи: {ent.TimeWork}\nДні роботи: {ent.DaysWork}\nПослуга: {ent.Poslygu}\nСайт: {ent.IsSayt}.", "Деталі"); ;
        }

    }
}
