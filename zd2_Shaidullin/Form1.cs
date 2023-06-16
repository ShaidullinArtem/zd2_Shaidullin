using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using zd2_Shaidullin.types;
using zd2_Shaidullin.utils;

namespace zd2_Shaidullin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private PhoneBook phoneBook = new PhoneBook();
        private Validator validator = new Validator();

        private void Form1_Load(object sender, EventArgs e)
        {
            PhoneBoookLoader.Load(phoneBook, "contacts.csv");
            viewTable();
        }
        
        private void viewTable()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = phoneBook.GetContacts();
        }
        
        private void add_btn_Click(object sender, EventArgs e)
        {
            Regex nameRegex = new Regex(@"^[а-яА-яa-zA-z]");
            Regex phoneRegex = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");

            string name = name_box.Text.Replace(" ", "");
            string phone = phone_box.Text.Replace(" ", "");

            ErrorStruct nameResponse = validator.RegexValidator(nameRegex, name, "Имя должно содержать только буквы");
            ErrorStruct phoneResponse = validator.RegexValidator(phoneRegex, phone,
                "Телефон должен соотвествовать российскому стандарту");

            if (nameResponse.IsValid && phoneResponse.IsValid)
            {
                bool check = false;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    DataGridViewCell cell = dataGridView1.Rows[i].Cells[0];

                    if (cell.Value != null && cell.Value.ToString() == name)
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    MessageBox.Show("Такое фио уже есть");
                    return;
                }
                else
                {

                    var contact = new Contact { Name = name, Phone = phone };

                    phoneBook.AddContact(contact);
                    viewTable();
                    name_box.Text = string.Empty;
                    phone_box.Text = string.Empty;

                }
            }
            else
            {
                if (!nameResponse.IsValid) MessageBox.Show(nameResponse.Message);
                if (!phoneResponse.IsValid) MessageBox.Show(phoneResponse.Message);
            }
        }

        private void remove_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var contact = (Contact)selectedRow.DataBoundItem;
                phoneBook.RemoveContact(contact);
                dataGridView1.Refresh();
            }
            viewTable();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            PhoneBoookLoader.Save(phoneBook, "contacts.csv");
            MessageBox.Show("Вы успешно сохранили");
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            var name = search_box.Text;
            var contacts = phoneBook.SearchContacts(name);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = contacts;
        }
    }
}