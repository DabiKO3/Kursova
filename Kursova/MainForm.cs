using Kursova.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova
{
    public partial class MainForm : Form
    {
        private Enterprises enterprises = new Enterprises();
        private Filters filters = new Filters();
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
            searchCategoryComboBox.DisplayMember = "Value"; 
            searchCategoryComboBox.ValueMember = "Key";

            searchCategoryComboBox.SelectedIndex = 0;

            dataGridView1.DataSource = enterprises.Data;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns["Name"].HeaderText = "Назва";
            dataGridView1.Columns["Rozryad"].HeaderText = "Розряд";
            dataGridView1.Columns["Address"].HeaderText = "Адреса";
            dataGridView1.Columns["Phone"].HeaderText = "Телефон";
            dataGridView1.Columns["Specialization"].HeaderText = "Спеціалізація";
            dataGridView1.Columns["Poslygu"].HeaderText = "Перелік послуг";
            dataGridView1.Columns["FormaVlasnosty"].HeaderText = "Форма власності";
            dataGridView1.Columns["TimeWork"].HeaderText = "Години роботи";
            dataGridView1.Columns["DaysWork"].HeaderText = "Дні роботи";
            dataGridView1.Columns["IsSayt"].HeaderText = "Є сайт";

            enterprises.AddEnterprise(new Enterprise("Прибирання та чистка", "I", "вул. Центральна, 1", "050-123-4567", "Прибирання", "9:00 - 18:00", "Пн-Пт", "Прибирання приміщень", "Приватна", true));
            enterprises.AddEnterprise(new Enterprise("Електросервіс", "II", "вул. Світла, 15", "067-234-5678", "Ремонт електрики", "8:00 - 17:00", "Пн-Сб", "Ремонт електромереж", "Державна", false));
            enterprises.AddEnterprise(new Enterprise("Прання і Хімчистка", "I", "вул. Пушкіна, 12", "073-345-6789", "Прання", "8:00 - 20:00", "Щодня", "Прання та хімчистка", "Приватна", true));
            enterprises.AddEnterprise(new Enterprise("Ремонт побутової техніки", "III", "вул. Київська, 27", "095-456-7890", "Ремонт", "9:00 - 18:00", "Пн-Пт", "Ремонт техніки", "Комунальна", false));
            enterprises.AddEnterprise(new Enterprise("Садівничий центр", "II", "вул. Садова, 8", "068-567-8901", "Садівництво", "8:00 - 19:00", "Пн-Сб", "Послуги з озеленення", "Приватна", true));
            enterprises.AddEnterprise(new Enterprise("Автосервіс", "III", "вул. Шевченка, 40", "050-678-9012", "Авто", "8:00 - 17:00", "Пн-Сб", "Ремонт автомобілів", "Кооператив", true));
            enterprises.AddEnterprise(new Enterprise("Сантехнічні послуги", "I", "вул. Лісова, 18", "093-789-0123", "Сантехніка", "8:00 - 18:00", "Пн-Пт", "Монтаж сантехніки", "Приватна", false));
            enterprises.AddEnterprise(new Enterprise("Послуги малярів", "II", "вул. Кольорова, 3", "066-890-1234", "Малярні роботи", "9:00 - 17:00", "Пн-Пт", "Фарбування стін", "Комунальна", false));
            enterprises.AddEnterprise(new Enterprise("Дезінфекційний сервіс", "I", "вул. Мирна, 21", "097-901-2345", "Дезінфекція", "8:00 - 20:00", "Пн-Пт", "Дезінфекція приміщень", "Приватна", true));
            enterprises.AddEnterprise(new Enterprise("Сервіс ремонтів", "III", "вул. Робітнича, 10", "050-012-3456", "Загальні послуги", "9:00 - 19:00", "Пн-Сб", "Майстер на годину", "Приватна", true));


            filters.AddFilter(new Filter("Name", "", ComparisonOperator.Contains));

        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void UpdateDataGridView()
        {


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
            const string message = "Ви точно хочете видалити всі елементи списку?";
            const string caption = "Увага!";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                enterprises.ClearEnterprise();
                UpdateDataGridView();
                LoadDataGrid();
            }
        }

        private void btnSaveXML_Click(object sender, EventArgs e)
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

        private void btnSaveTXT_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстові файли (*.txt) |*.txt|All files (*.*) |*.*";
                saveFileDialog.Title = "Зберегти дані у текстовому форматі";
                saveFileDialog.InitialDirectory = Application.StartupPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileIOServis fileIOServis = new FileIOServis(saveFileDialog.FileName);
                    var succsesfully = fileIOServis.SaveDateTXT(enterprises.Data);
                    if (succsesfully.Item1)
                    {
                        MessageBox.Show("Дані успішно збережені в текстовий файл!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Сталась помилка: \n{0}", succsesfully.Item2.Message,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void btnSaveBin_Click(object sender, EventArgs e)
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
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML файли (*.xml)|*.xml|Усі файли (*.*)|*.*";
                saveFileDialog.Title = "Виберіть місце для збереження файлу";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileIOServis fileIOServis = new FileIOServis(saveFileDialog.FileName);
                    enterprises.Data = fileIOServis.LoadFromXML();

                    MessageBox.Show("Файл успішно збережений!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void loadXML_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "XML файли (*.xml)|*.xml|Усі файли (*.*)|*.*";
                openFileDialog.Title = "Прочитати дані у XML форматі";
                openFileDialog.InitialDirectory = Application.StartupPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileIOServis fileIOServis = new FileIOServis(openFileDialog.FileName);
                    enterprises.Data = fileIOServis.LoadFromXML();
                    UpdateDataGridView();
                }
            }
        }

        private void loadBin_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "Файли даних (*.bin) |*.bin|All files (*.*) |*.*";
                openFileDialog.Title = "Прочитати дані у бінарному форматі";
                openFileDialog.InitialDirectory = Application.StartupPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileIOServis fileIOServis = new FileIOServis(openFileDialog.FileName);
                    enterprises.Data = fileIOServis.LoadFromBinary();
                    UpdateDataGridView();
                }
            }
        }

        private void loadTXT_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "Файли даних (*.txt) |*.txt|All files (*.*) |*.*";
                openFileDialog.Title = "Прочитати дані у текстовому форматі";
                openFileDialog.InitialDirectory = Application.StartupPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileIOServis fileIOServis = new FileIOServis(openFileDialog.FileName);
                    enterprises.Data = fileIOServis.LoadFromTXT();
                }
            }
        }

        private void search_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filteredEnterprises = enterprises.Data.AsEnumerable();
            BindingList<Enterprise> newEnterprises = new BindingList<Enterprise>();

            foreach (var filter in filters.Data)
            {
                var field = filter.Field;
                filteredEnterprises = filteredEnterprises.Where(ent =>
                {
                    var value = GetEnterpriseFieldValue(ent, field)?.ToString() ?? string.Empty;

                    bool result;

                    if (filter.Operator == ComparisonOperator.Equals)
                    {
                        result = string.Equals(value, filter.Value, StringComparison.OrdinalIgnoreCase);
                    }
                    else if (filter.Operator == ComparisonOperator.Contains)
                    {
                        result = value?.IndexOf(filter.Value, StringComparison.OrdinalIgnoreCase) >= 0;
                    }
                    else if (filter.Operator == ComparisonOperator.notEquals)
                    {
                        result = !string.Equals(value, filter.Value, StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        result = true; 
                    }

                    return result;
                });

            }

            foreach (var ent in filteredEnterprises)
            {
                newEnterprises.Add(ent);
            }

            
            dataGridView1.DataSource = newEnterprises;
        }

        private object GetEnterpriseFieldValue(Enterprise enterprise, string fieldName)
        {

            var property = typeof(Enterprise).GetProperty(fieldName);
            return property?.GetValue(enterprise);
        }

        private void searchCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filters.Data.Count > 0)
            {
                
                filters.Data[0].Field = ((KeyValuePair<string, string>)searchCategoryComboBox.SelectedItem).Key;
            }
        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {
            if (filters.Data.Count > 0)
            {
              
                filters.Data[0].Value = searchText.Text.ToString();
            }
        }

        private void canselSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = enterprises.Data;
        }

        private void filters_Click(object sender, EventArgs e)
        {
            var filerstForm = new FormFilters(filters.Data);
            if (filerstForm.ShowDialog() == DialogResult.OK)
            {
                filters.Data = filerstForm.Filters;
                if (filters.Data.Count > 0)
                {
                    searchCategoryComboBox.SelectedValue = filters.Data[0].Field;
                    searchText.Text = filters.Data[0].Value;
                }
            }
        }

 
    }
}
