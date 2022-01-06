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
    public partial class Form10_Edit : Form
    {
        MySqlConnection conn;
        public Form10_Edit()
        {
            InitializeComponent();
        }
        public void SelectData()
        {
            conn.Open();
            this.Text = $"Редактор сотрудника ID: {ControlData.ID_TICKET}";
            string sql_select_current_employee = $"SELECT id_employee, fio_employee, post_employee FROM employees WHERE id_employee = {ControlData.ID_TICKET}";
            MySqlCommand command = new MySqlCommand(sql_select_current_employee, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
            }
            reader.Close();
            conn.Close();
        }
        private void Form10_Edit_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            SelectData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string new_fio_employee = textBox1.Text;
            string new_post_employee = textBox2.Text;
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите все данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql_update_current_employee = $"UPDATE employees SET fio_employee='{new_fio_employee}', post_employee='{new_post_employee}'" +
                $"WHERE (id_employee='{ControlData.ID_TICKET}')";
                conn.Open();
                MySqlCommand command = new MySqlCommand(sql_update_current_employee, conn);
                command.ExecuteNonQuery();
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