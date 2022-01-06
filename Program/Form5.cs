using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Program
{
    public partial class Form5 : Form
    {
        public void GetListEmployees(ListBox listbox)
        {
            listbox.Items.Clear();
            conn.Open();
            string sql = $"SELECT * FROM employees";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listbox.Items.Add($"{reader[0].ToString()}) ФИО: {reader[1].ToString()} | Должность: {reader[2].ToString()}");
            }
            reader.Close();
            conn.Close();
        }
        public bool InsertEmployees (string fio_employee, string post_employee)
        {
            int InsertCount = 0;
            bool result = false;
            conn.Open();
            string query = $"INSERT INTO employees (fio_employee, post_employee) VALUES ('{fio_employee}', '{post_employee}')";
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                InsertCount = command.ExecuteNonQuery();
            }
            catch
            {
                InsertCount = 0;
            }
            finally
            {
                conn.Close();
                if (InsertCount != 0)
                {
                    result = true;
                }
            }
            return result;
        }
        MySqlConnection conn;

        public Form5()
        {
            InitializeComponent();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            GetListEmployees(listBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ins_fio = textBox1.Text;
            string ins_post = textBox2.Text;
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (InsertEmployees(ins_fio, ins_post))
                {
                    GetListEmployees(listBox1);
                    MessageBox.Show("Сотрудник успешно добавлен!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}