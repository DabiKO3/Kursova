using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova
{
    public partial class AddForm : Form
    {
        public Enterprise Enterprise { get; private set; }
        public AddForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            Enterprise = new Enterprise();
        }
        public AddForm(Enterprise enterprise) : this()
        {
            Enterprise = enterprise;
            LoadEnterpriseData();
        }
        private void InitializeCustomComponents()
        {
            // Додавання компонентів до форми
            this.Controls.Add(tbName);
            this.Controls.Add(tbRozryad);
            this.Controls.Add(tbAdresa);
            this.Controls.Add(tbPhone);
            this.Controls.Add(tbSpecial);
            this.Controls.Add(tbTimeWork);
            this.Controls.Add(tbDaysWork);
            this.Controls.Add(cbPoslygu);
            this.Controls.Add(cbFormaVlasnosty);
            this.Controls.Add(btnSaveinfo);
            this.Controls.Add(btnGoback);


            btnSaveinfo.Click += new EventHandler(btnSaveinfo_Click);
            btnGoback.Click += new EventHandler(btnGoback_Click);

            cbPoslygu.Items.AddRange(new string[]
            {
                "Ремонт та обслуговування",
                "Прибирання",
                "Краса та здоров'я",
                "Консультації",
                "Навчання",
            });

            cbFormaVlasnosty.Items.AddRange(new string[]
            {
                "Приватна",
                "Державна",
                "Комунальна",
                "Змішана"
            });

            btnSaveinfo.Click += new EventHandler(btnSaveinfo_Click);
            btnGoback.Click += new EventHandler(btnGoback_Click);
        }
        private void LoadEnterpriseData()
        {
            tbName.Text = Enterprise.Name;
            tbRozryad.Text = Enterprise.Rozryad;
            tbAdresa.Text = Enterprise.Address;
            tbPhone.Text = Enterprise.Phone;
            tbSpecial.Text = Enterprise.Specialization;
            tbTimeWork.Text = Enterprise.TimeWork;
            tbDaysWork.Text = Enterprise.DaysWork;
            cbPoslygu.SelectedItem = Enterprise.Poslygu;
            cbFormaVlasnosty.SelectedItem = Enterprise.FormaVlasnosty;
        }
        private void btnSaveinfo_Click(object sender, EventArgs e)
        {
            Enterprise.Name = tbName.Text;
            Enterprise.Rozryad = tbRozryad.Text;
            Enterprise.Address = tbAdresa.Text;
            Enterprise.Phone = tbPhone.Text;
            Enterprise.Specialization = tbSpecial.Text;
            Enterprise.TimeWork = tbTimeWork.Text;
            Enterprise.DaysWork = tbDaysWork.Text;
            Enterprise.Poslygu = cbPoslygu.SelectedItem != null ? cbPoslygu.SelectedItem.ToString() : "";
            Enterprise.FormaVlasnosty = cbFormaVlasnosty.SelectedItem != null ? cbFormaVlasnosty.SelectedItem.ToString() : "";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnGoback_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
