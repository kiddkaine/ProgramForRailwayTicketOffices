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
    public partial class Form4 : Form
    {
        public void GetListClients(ListBox listbox)
        {
            listbox.Items.Clear();
            conn.Open();
            string sql = $"SELECT * FROM clients";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id_client = reader[0].ToString();
                string fio_client = reader[1].ToString();
                string pass_client = reader[2].ToString();
                string privilege_client = reader[3].ToString();

                listbox.Items.Add($"{id_client}) ФИО: {fio_client}, паспорт: {pass_client}, уровень льготы: {privilege_client}");
            }
            reader.Close();
            conn.Close();
        }
        public bool DeleteClients(string id_client)
        {
            int InsertCount = 0;
            bool result = false;
            conn.Open();
            string query = $"DELETE FROM clients WHERE (id_client='{id_client}')";
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
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            GetListClients(listBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id_delete = textBox1.Text;
            if (DeleteClients(id_delete))
            {
                GetListClients(listBox1);
                MessageBox.Show("Клиент успешно удалён!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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