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
    public partial class Form9_Edit : Form
    {
        MySqlConnection conn;
        public Form9_Edit()
        {
            InitializeComponent();
        }
        public void SelectData()
        {
            conn.Open();
            this.Text = $"Редактор клиента ID: {ControlData.ID}";
            string sql_select_current_client = $"SELECT id_client, fio_client, pass_client, id_privilege FROM clients WHERE id_client = {ControlData.ID}";
            MySqlCommand command = new MySqlCommand(sql_select_current_client, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
                comboBox1.SelectedValue = reader[3].ToString();
            }
            reader.Close();
            conn.Close();
        }
        public void GetComboBox1()
        {
            DataTable list_client_table = new DataTable();
            MySqlCommand list_client_command = new MySqlCommand();
            conn.Open();
            list_client_table.Columns.Add(new DataColumn("id_privilege", System.Type.GetType("System.Int32")));
            list_client_table.Columns.Add(new DataColumn("availability", System.Type.GetType("System.String")));
            comboBox1.DataSource = list_client_table;
            comboBox1.DisplayMember = "availability";
            comboBox1.ValueMember = "id_privilege";
            string sql_list_clients = "SELECT id_privilege, availability FROM privilege";
            list_client_command.CommandText = sql_list_clients;
            list_client_command.Connection = conn;
            MySqlDataReader list_client_reader;
            try
            {
                list_client_reader = list_client_command.ExecuteReader();
                while (list_client_reader.Read())
                {
                    DataRow rowToAdd = list_client_table.NewRow();
                    rowToAdd["id_privilege"] = Convert.ToInt32(list_client_reader[0]);
                    rowToAdd["availability"] = list_client_reader[1].ToString();
                    list_client_table.Rows.Add(rowToAdd);
                }
                list_client_reader.Close();
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

        private void Form9_Edit_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            GetComboBox1();
            SelectData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string new_fio_client = textBox1.Text;
            string new_pass_client = textBox2.Text;
            string new_id_privilege = comboBox1.SelectedValue.ToString();
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите все данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql_update_current_client = $"UPDATE clients SET fio_client='{new_fio_client}', pass_client='{new_pass_client}', id_privilege='{new_id_privilege}'" +
                $"WHERE (id_client='{ControlData.ID}')";
                conn.Open();
                MySqlCommand command= new MySqlCommand(sql_update_current_client, conn);
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