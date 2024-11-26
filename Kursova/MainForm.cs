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

            generateColumns();

            enterprises.AddEnterprise(new Enterprise("Прибирання та чистка", 1, "вул. Центральна, 1", "050-123-4567", "Прибирання", "9:00 - 18:00", "Пн-Пт", "Прибирання приміщень", "Приватна", true, 5));
            enterprises.AddEnterprise(new Enterprise("Електросервіс", 2, "вул. Світла, 15", "067-234-5678", "Ремонт електрики", "8:00 - 17:00", "Пн-Сб", "Ремонт електромереж", "Державна", false, 4));
            enterprises.AddEnterprise(new Enterprise("Прання і Хімчистка", 3, "вул. Пушкіна, 12", "073-345-6789", "Прання", "8:00 - 20:00", "Щодня", "Прання та хімчистка", "Приватна", true,5));
            enterprises.AddEnterprise(new Enterprise("Ремонт побутової техніки", 3, "вул. Київська, 27", "095-456-7890", "Ремонт", "9:00 - 18:00", "Пн-Пт", "Ремонт техніки", "Комунальна", false,3));
            enterprises.AddEnterprise(new Enterprise("Садівничий центр", 2, "вул. Садова, 8", "068-567-8901", "Садівництво", "8:00 - 19:00", "Пн-Сб", "Послуги з озеленення", "Приватна", true, 1));
            enterprises.AddEnterprise(new Enterprise("Автосервіс", 3, "вул. Шевченка, 40", "050-678-9012", "Авто", "8:00 - 17:00", "Пн-Сб", "Ремонт автомобілів", "Кооператив", true, 4));
            enterprises.AddEnterprise(new Enterprise("Сантехнічні послуги", 1, "вул. Лісова, 18", "093-789-0123", "Сантехніка", "8:00 - 18:00", "Пн-Пт", "Монтаж сантехніки", "Приватна", false, 3));
            enterprises.AddEnterprise(new Enterprise("Послуги малярів", 2, "вул. Кольорова, 3", "066-890-1234", "Малярні роботи", "9:00 - 17:00", "Пн-Пт", "Фарбування стін", "Комунальна", false, 4));
            enterprises.AddEnterprise(new Enterprise("Дезінфекційний сервіс", 1, "вул. Мирна, 21", "097-901-2345", "Дезінфекція", "8:00 - 20:00", "Пн-Пт", "Дезінфекція приміщень", "Приватна", true,2));
            Enterprise ent = new Enterprise("Сервіс ремонтів", 3, "вул. Робітнича, 10", "050-012-3456", "Загальні послуги", "9:00 - 19:00", "Пн-Сб", "Майстер на годину", "Приватна", true, 1);
            enterprises.AddEnterprise(ent);

            filters.AddFilter(new Filter("Name", "", ComparisonOperator.Contains));

        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Application.Exit();
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

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditForm = new AddForm();
            if (addEditForm.ShowDialog() == DialogResult.OK)
            {
                enterprises.AddEnterprise(addEditForm.Enterprise);
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
            addEditForm.ShowDialog();

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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedEnterprise = (Enterprise)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                var addEditForm = new AddForm(selectedEnterprise);
                addEditForm.ShowDialog();
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
                }
            }

            dataGridView1.DataSource = enterprises.Data;
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

            dataGridView1.DataSource = enterprises.Data;

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

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void clearTerm_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var selectedEnterprise = (Enterprise)dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem;
            selectedEnterprise.Clear();
        }

        private void displayInfo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var selectedEnterprise = (Enterprise)dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem;
            selectedEnterprise.DisplayInfo();
        }

        private void displayDetails_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var selectedEnterprise = (Enterprise)dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem;
            selectedEnterprise.DisplayDetails();
        }


        private void staticDisplayInfo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var selectedEnterprise = (Enterprise)dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem;
            Enterprise.DisplayInfo(selectedEnterprise.Name);
        }

        private void generateColumns()
        {
            dataGridView1.Columns.Clear();

            DataGridViewColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "Name";
            nameColumn.HeaderText = "Назва";
            dataGridView1.Columns.Add(nameColumn);

            DataGridViewColumn rozryadColumn = new DataGridViewTextBoxColumn();
            rozryadColumn.DataPropertyName = "Rozryad";
            rozryadColumn.HeaderText = "Розряд";
            dataGridView1.Columns.Add(rozryadColumn);

            DataGridViewColumn addressColumn = new DataGridViewTextBoxColumn();
            addressColumn.DataPropertyName = "Address";
            addressColumn.HeaderText = "Адреса";
            dataGridView1.Columns.Add(addressColumn);

            DataGridViewColumn phoneColumn = new DataGridViewTextBoxColumn();
            phoneColumn.DataPropertyName = "Phone";
            phoneColumn.HeaderText = "Телефон";
            dataGridView1.Columns.Add(phoneColumn);

            DataGridViewColumn specializationColumn = new DataGridViewTextBoxColumn();
            specializationColumn.DataPropertyName = "Specialization";
            specializationColumn.HeaderText = "Спеціалізація";
            dataGridView1.Columns.Add(specializationColumn);

            DataGridViewColumn poslyguColumn = new DataGridViewTextBoxColumn();
            poslyguColumn.DataPropertyName = "Poslygu";
            poslyguColumn.HeaderText = "Перелік послуг";
            dataGridView1.Columns.Add(poslyguColumn);

            DataGridViewColumn formaVlasnostyColumn = new DataGridViewTextBoxColumn();
            formaVlasnostyColumn.DataPropertyName = "FormaVlasnosty";
            formaVlasnostyColumn.HeaderText = "Форма власності";
            dataGridView1.Columns.Add(formaVlasnostyColumn);

            DataGridViewColumn timeWorkColumn = new DataGridViewTextBoxColumn();
            timeWorkColumn.DataPropertyName = "TimeWork";
            timeWorkColumn.HeaderText = "Години роботи";
            dataGridView1.Columns.Add(timeWorkColumn);

            DataGridViewColumn daysWorkColumn = new DataGridViewTextBoxColumn();
            daysWorkColumn.DataPropertyName = "DaysWork";
            daysWorkColumn.HeaderText = "Дні роботи";
            dataGridView1.Columns.Add(daysWorkColumn);

            DataGridViewColumn isSaytColumn = new DataGridViewCheckBoxColumn();
            isSaytColumn.DataPropertyName = "IsSayt";
            isSaytColumn.HeaderText = "Є сайт";
            dataGridView1.Columns.Add(isSaytColumn);

          
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
