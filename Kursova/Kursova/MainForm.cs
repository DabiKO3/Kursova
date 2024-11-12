using Kursova.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Kursova
{
    public partial class MainForm : Form
    {
        private Enterprises enterprises = new Enterprises();
        private Dictionary<string, string> columnDisplayNames;
        private bool checkBoxSearch;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeCustomComponents()
        {

            columnDisplayNames = new Dictionary<string, string>
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

            searchCategoryComboBox.DataSource = new BindingSource(columnDisplayNames, null);
            searchCategoryComboBox.DisplayMember = "Value"; // Відображення користувацької назви
            searchCategoryComboBox.ValueMember = "Key";

            searchCategoryComboBox.SelectedIndex = 0;

            searchCategoryComboBox.SelectedIndexChanged += searchCategoryComboBox_SelectedIndexChanged;

            dataGridView1.DataSource = enterprises.Data;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AllowUserToAddRows = false;
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }
        
        private void UpdateDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = enterprises.Data;
        }
        
        private void LoadDataGrid()
        {
            if (!checkBoxSearch)
            {
                dataGridView1.DataSource = enterprises.Data;
                return;
            }

            string searchCategory = ((KeyValuePair<string, string>)searchCategoryComboBox.SelectedItem).Key;
            string searchTerm = searchText.Text.ToLower();

            var filteredEnterprises = enterprises.FindEnterprises(searchTerm, searchCategory);
            dataGridView1.DataSource = filteredEnterprises;

        }

        private void searchCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)

        {
            searchText.Text = "";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
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

            var selectedEnterprise = (Enterprise)dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem;
            enterprises.RemoveEnterprise(selectedEnterprise);
            UpdateDataGridView();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditForm = new AddForm();
            if (addEditForm.ShowDialog() == DialogResult.OK)
            {
                enterprises.AddEnterprise(addEditForm.Enterprise);
                UpdateDataGridView();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var selectedEnterprise = (Enterprise)dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem;
            var addEditForm = new AddForm(selectedEnterprise);
            if (addEditForm.ShowDialog() == DialogResult.OK)
            {
                UpdateDataGridView();
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            enterprises.ClearEnterprise();
            UpdateDataGridView();
            LoadDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML файли (*.xml)|*.xml|Усі файли (*.*)|*.*";
                saveFileDialog.Title = "Виберіть місце для збереження файлу";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileIOServis fileIOServis = new FileIOServis(saveFileDialog.FileName);
                    fileIOServis.SaveDateXML(enterprises.Data);
                  
                    MessageBox.Show("Файл успішно збережений!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
                saveFileDialog.Title = "Виберіть місце для збереження текстового файлу";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //string jsonString = JsonSerializer.Serialize(enterprises.Data, new JsonSerializerOptions { WriteIndented = true });
                    //File.WriteAllText(saveFileDialog.FileName, jsonString);

                    MessageBox.Show("Дані успішно збережені в текстовий файл!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Binary файли (*.bin)|*.bin|Усі файли (*.*)|*.*";
                saveFileDialog.Title = "Виберіть місце для збереження бінарного файлу";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileIOServis fileIOServis = new FileIOServis(saveFileDialog.FileName);
                    fileIOServis.SaveDateBinary(enterprises.Data);

                    MessageBox.Show("Дані успішно збережені в бінарний файл!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            checkBoxSearch = checkBox1.Checked;

            LoadDataGrid();
        }
        
        private void searchText_TextChanged(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedEnterprise = (Enterprise)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                var addEditForm = new AddForm(selectedEnterprise);
                if (addEditForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateDataGridView();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }
    }
}