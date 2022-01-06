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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        MySqlConnection conn;
        private void Form2_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;" +
                "database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string selected_id_employee = textBox1.Text;
            conn.Open();
            string sql = $"SELECT fio_employee, post_employee FROM employees WHERE id_employee={selected_id_employee}";
            MySqlCommand command = new MySqlCommand(sql, conn);
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    listBox1.Items.Add("ФИО сотрудника: " + reader[0].ToString());
                    listBox1.Items.Add("Должность: " + reader[1].ToString());
                    listBox1.Items.Add("----------------------------------------------------------------------------------------------------");
                    textBox2.Text = reader[0].ToString();
                    textBox3.Text = reader[1].ToString();
                    reader.Close();
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    MessageBox.Show($"Данные о сотруднике с ID {selected_id_employee} отсутствуют.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                MessageBox.Show($"Заполните поле ID!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            conn.Open();
            string sql = $"SELECT fio_employee, post_employee FROM employees";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add("ФИО сотрудника: " + reader[0].ToString());
                listBox1.Items.Add("Должность: " + reader[1].ToString());
                listBox1.Items.Add("----------------------------------------------------------------------------------------------------");
            }
            reader.Close();
            conn.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string redact_id = textBox1.Text;
            string new_fio = textBox2.Text;
            string new_post = textBox3.Text;
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Введите данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                conn.Open();
                string query2 = $"UPDATE employees SET fio_employee = '{new_fio}', post_employee = '{new_post}' WHERE id_employee = {redact_id}";
                MySqlCommand command = new MySqlCommand(query2, conn);
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Изменение прошло успешно.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Введите данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}