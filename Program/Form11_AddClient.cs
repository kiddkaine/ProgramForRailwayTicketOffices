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
    public partial class Form11_AddClient : Form
    {
        MySqlConnection conn;
        public Form11_AddClient()
        {
            InitializeComponent();
        }
        public void GetComboBox1()
        {
            DataTable list_train_table = new DataTable();
            MySqlCommand list_train_command = new MySqlCommand();
            conn.Open();
            list_train_table.Columns.Add(new DataColumn("id_privilege", System.Type.GetType("System.Int32")));
            list_train_table.Columns.Add(new DataColumn("availability", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox1.DataSource = list_train_table;
            comboBox1.DisplayMember = "availability";
            comboBox1.ValueMember = "id_privilege";
            string sql_list_users = "SELECT id_privilege, availability FROM privilege";
            list_train_command.CommandText = sql_list_users;
            list_train_command.Connection = conn;
            MySqlDataReader list_train_reader;
            try
            {
                list_train_reader = list_train_command.ExecuteReader();
                while (list_train_reader.Read())
                {
                    DataRow rowToAdd = list_train_table.NewRow();
                    rowToAdd["id_privilege"] = Convert.ToInt32(list_train_reader[0]);
                    rowToAdd["availability"] = list_train_reader[1].ToString();
                    list_train_table.Rows.Add(rowToAdd);
                }
                list_train_reader.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка чтения списка.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn.Close();
            }
        }
        private void Form11_AddClient_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";

            conn = new MySqlConnection(connStr);

            GetComboBox1();
            comboBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string new_fio_client = textBox1.Text;
            string new_pass_client = textBox2.Text;
            string new_id_privilege = comboBox1.SelectedValue.ToString();
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Введите данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql_update_current_stud = $"INSERT INTO clients (fio_client, pass_client, id_privilege) " +
                                              $"VALUES ('{new_fio_client}', '{new_pass_client}', '{new_id_privilege}'); " +
                                              $"SELECT id_client FROM clients WHERE (id_client = LAST_INSERT_ID());";
                conn.Open();
                MySqlCommand command = new MySqlCommand(sql_update_current_stud, conn);
                string id_insert_client = command.ExecuteScalar().ToString();
                SomeClass.new_inserted_id = id_insert_client;
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