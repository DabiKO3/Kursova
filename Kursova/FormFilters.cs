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
    public partial class FormFilters : Form
    {
        public BindingList<Filter> Filters { get; set; }
        public FormFilters()
        {
            InitializeComponent();
            Filters = new BindingList<Filter>();
        }

        public FormFilters(BindingList<Filter> filters) : this()
        {
            foreach (var filter in filters)
                Filters.Add(filter);

            dataGridViewFilters.DataSource = Filters;
            dataGridViewFilters.AllowUserToAddRows = false;
            dataGridViewFilters.AutoGenerateColumns = false;

            Dictionary<string, string> fieldNames = new Dictionary<string, string>
            {
                { "Name", "Назва" },
                { "Rozryad", "Розряд" },
                { "Address", "Адреса" },
                { "Phone", "Телефон" },
                { "Specialization", "Спеціалізація" },
                { "TimeWork", "Години роботи" },
                { "DaysWork", "Дні роботи" },
                { "Poslygu", "Перелік наданих послуг" },
                { "FormaVlasnosty", "Форма власності" }
            };

            if (dataGridViewFilters.Columns.Contains("Field"))
            {
                dataGridViewFilters.Columns.Remove("Field");
            }

            DataGridViewComboBoxColumn fieldColumn = new DataGridViewComboBoxColumn();
            fieldColumn.DataPropertyName = "Field";  
            fieldColumn.HeaderText = "Імя поля";
            fieldColumn.DataSource = new BindingSource(fieldNames, null);  
            fieldColumn.DisplayMember = "Value";     
            fieldColumn.ValueMember = "Key";        

            dataGridViewFilters.Columns.Insert(0, fieldColumn);

            Dictionary<ComparisonOperator, string> operatorNames = new Dictionary<ComparisonOperator, string>
            {
                { ComparisonOperator.Equals, "Дорівнює" },
                { ComparisonOperator.Contains, "Містить" },
                { ComparisonOperator.notEquals, "Не дорівнює" }
            };

            if (dataGridViewFilters.Columns.Contains("Operator"))
            {
                dataGridViewFilters.Columns.Remove("Operator");
            }

            DataGridViewComboBoxColumn operatorColumn = new DataGridViewComboBoxColumn();
            operatorColumn.DataPropertyName = "Operator";
            operatorColumn.HeaderText = "Вид порівняння";
            operatorColumn.DataSource = new BindingSource(operatorNames, null);
            operatorColumn.DisplayMember = "Value";
            operatorColumn.ValueMember = "Key";

            dataGridViewFilters.Columns.Insert(1, operatorColumn);

            dataGridViewFilters.Columns["Value"].HeaderText = "";
            dataGridViewFilters.Columns["Value"].DataPropertyName = "Value";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Filters.Add(new Filter("Name", "", ComparisonOperator.Contains));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (dataGridViewFilters.CurrentRow == null)
            {
                return;
            }

            const string message =
            "Ви точно хочете видалити елемент списку?";
            const string caption = "Увага!";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            var selectedFilter = (Filter)dataGridViewFilters.Rows[dataGridViewFilters.CurrentRow.Index].DataBoundItem;
            Filters.Remove(selectedFilter);
        }

       
    }
}
