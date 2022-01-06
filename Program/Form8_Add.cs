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
    public partial class Form8_Add : Form
    {
        MySqlConnection conn;
        public Form8_Add()
        {
            InitializeComponent();
        }
        public void GetComboBox1()
        {
            DataTable list_flight_table = new DataTable();
            MySqlCommand list_flight_command = new MySqlCommand();
            conn.Open();
            list_flight_table.Columns.Add(new DataColumn("id_state", System.Type.GetType("System.Int32")));
            list_flight_table.Columns.Add(new DataColumn("title_state", System.Type.GetType("System.String")));
            comboBox1.DataSource = list_flight_table;
            comboBox1.DisplayMember = "title_state";
            comboBox1.ValueMember = "id_state";
            string sql_list_flight = "SELECT id_state, title_state FROM states";
            list_flight_command.CommandText = sql_list_flight;
            list_flight_command.Connection = conn;
            MySqlDataReader list_flight_reader;
            try
            {
                list_flight_reader = list_flight_command.ExecuteReader();
                while (list_flight_reader.Read())
                {
                    DataRow rowToAdd = list_flight_table.NewRow();
                    rowToAdd["id_state"] = Convert.ToInt32(list_flight_reader[0]);
                    rowToAdd["title_state"] = list_flight_reader[1].ToString();
                    list_flight_table.Rows.Add(rowToAdd);
                }
                list_flight_reader.Close();
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

        private void Form8_Add_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            GetComboBox1();
            comboBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text) || String.IsNullOrWhiteSpace(textBox5.Text) || String.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Введите данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql_update_current_flight = $"INSERT INTO flights (route_number, price, departure_station, departure_date, arrival_station, arrival_date, id_state) " +
                                                $"VALUES (@route_number, @price, @departure_station, @departure_date, @arrival_station, @arrival_date, @id_state)";
                using (MySqlCommand command = new MySqlCommand(sql_update_current_flight, conn))
                {
                    command.Parameters.Add("@route_number", MySqlDbType.VarChar).Value = textBox1.Text;
                    command.Parameters.Add("@price", MySqlDbType.VarChar).Value = textBox2.Text;
                    command.Parameters.Add("@departure_station", MySqlDbType.VarChar).Value = textBox3.Text;
                    command.Parameters.Add("@departure_date", MySqlDbType.DateTime).Value = dateTimePicker1.Value;
                    command.Parameters.Add("@arrival_station", MySqlDbType.VarChar).Value = textBox5.Text;
                    command.Parameters.Add("@arrival_date", MySqlDbType.DateTime).Value = dateTimePicker2.Value;
                    command.Parameters.Add("@id_state", MySqlDbType.VarChar).Value = comboBox1.SelectedValue.ToString();
                    conn.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Рейс успешно добавлен!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Введите данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox5.Clear();
                    Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}