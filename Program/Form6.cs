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
    public partial class Form6 : Form
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
                string id_employee = reader[0].ToString();
                string fio_employee = reader[1].ToString();
                string post_employee = reader[2].ToString();

                listbox.Items.Add($"{id_employee}) ФИО: {fio_employee}, должность: {post_employee}");
            }
            reader.Close();
            conn.Close();
        }
        public bool DeleteEmployees(string id_employee)
        {
            int InsertCount = 0;
            bool result = false;
            conn.Open();
            string query = $"DELETE FROM employees WHERE (id_employee='{id_employee}')";
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
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            GetListEmployees(listBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id_delete = textBox1.Text;
            if (DeleteEmployees(id_delete))
            {
                GetListEmployees(listBox1);
                MessageBox.Show("Сотрудник уволен!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите ID!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Некорректное значение!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            textBox1.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}