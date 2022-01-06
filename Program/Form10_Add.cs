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
    public partial class Form10_Add : Form
    {
        MySqlConnection conn;
        public Form10_Add()
        {
            InitializeComponent();
        }
        private void Form10_Add_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string new_fio_employee = textBox1.Text;
            string new_post_employee = textBox2.Text;
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql_update_current_employee = $"INSERT INTO employees (fio_employee, post_employee) " +
                                $"VALUES ('{new_fio_employee}', '{new_post_employee}')";
                conn.Open();
                MySqlCommand command = new MySqlCommand(sql_update_current_employee, conn);
                command.ExecuteNonQuery();
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show($"Сотрудник успешно добавлен!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}