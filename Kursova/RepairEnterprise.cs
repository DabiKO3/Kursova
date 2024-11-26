using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova
{
    [Serializable]
    public class RepairEnterprise : Enterprise
    {
        private int numberEmployees;
        public int NumberEmployees
        { 
            get { return numberEmployees; }
            set { numberEmployees = value; } 
        }
        
        public RepairEnterprise() : base() { }

        public RepairEnterprise(Enterprise enterprise, int numberEmployees) 
            : base(enterprise.Name, enterprise.Rozryad, enterprise.Address ,enterprise.Phone,
                  enterprise.Specialization, enterprise.TimeWork, enterprise.DaysWork, enterprise.Poslygu,
                  enterprise.FormaVlasnosty,enterprise.IsSayt, enterprise.QualitSrvices) 
        {
            this.numberEmployees = numberEmployees;
        }

        public RepairEnterprise(string name, int rozryad, string address, string phone,
            string specialization, string timeWork, string daysWork, string poslygu,
            string formaVlasnosty, bool isSayt, double qualitSrvices, int numberEmployees)
            : base(name, rozryad ,address, phone, specialization,timeWork,daysWork,poslygu,formaVlasnosty,isSayt,qualitSrvices) // Вызов конструктора базового класса
        {
            this.numberEmployees = numberEmployees;
        }
         //12.
        public override void DisplayInfo()
        {
            MessageBox.Show($"Назва: {Name}\nАдрес: {Address}\nТелефон: {Phone}\nСпеціалізація: {Specialization}\nФорма властності: {FormaVlasnosty}\nРозряд: {Rozryad}\nЧас роботи: {TimeWork}\nДні роботи: {DaysWork}\nПослуга: {Poslygu}\nСайт: {IsSayt}\nКількість співробітників: {numberEmployees}.", "Інформація"); ;
        }

    }
}
