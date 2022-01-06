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
    public partial class Form3 : Form
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
                listbox.Items.Add($"{reader[0].ToString()}) ФИО: {reader[1].ToString()} | Паспорт: {reader[2].ToString()} | Уровень льготы: {reader[3].ToString()}");
            }
            reader.Close();
            conn.Close();
        }
        public bool InsertClients(string fio_client, string pass_client, string privilege_client)
        {
            int InsertCount = 0;
            bool result = false;
            conn.Open();
            string query = $"INSERT INTO clients (fio_client, pass_client, id_privilege) VALUES ('{fio_client}', '{pass_client}', '{privilege_client}')";
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

        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            GetListClients(listBox1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string ins_fio = textBox1.Text;
            string ins_pass = textBox2.Text;
            string ins_privilege = textBox3.Text;
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Введите данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (InsertClients(ins_fio, ins_pass, ins_privilege))
                {
                    GetListClients(listBox1);
                    MessageBox.Show("Клиент успешно добавлен!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}