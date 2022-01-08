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
    public partial class Form7 : Form
    {
        MySqlConnection conn;
        string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
        public void GetComboBoxList()
        {
            DataTable list_train_table = new DataTable();
            MySqlCommand list_train_command = new MySqlCommand();
            conn.Open();
            list_train_table.Columns.Add(new DataColumn("id_train", System.Type.GetType("System.Int32")));
            list_train_table.Columns.Add(new DataColumn("route_number", System.Type.GetType("System.String")));
            comboBox1.DataSource = list_train_table;
            comboBox1.DisplayMember = "route_number";
            comboBox1.ValueMember = "id_train";
            string sql_list_users = "SELECT id_train, route_number FROM flights";
            list_train_command.CommandText = sql_list_users;
            list_train_command.Connection = conn;
            MySqlDataReader list_train_reader;
            try
            {
                list_train_reader = list_train_command.ExecuteReader();
                while (list_train_reader.Read())
                {
                    DataRow rowToAdd = list_train_table.NewRow();
                    rowToAdd["id_train"] = Convert.ToInt32(list_train_reader[0]);
                    rowToAdd["route_number"] = list_train_reader[1].ToString();
                    list_train_table.Rows.Add(rowToAdd);
                }
                list_train_reader.Close();
                conn.Close();
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
        public void GetComboBox1()
        {
            DataTable list_flight_table = new DataTable();
            MySqlCommand list_flight_command = new MySqlCommand();
            conn.Open();
            list_flight_table.Columns.Add(new DataColumn("id_state", System.Type.GetType("System.Int32")));
            list_flight_table.Columns.Add(new DataColumn("title_state", System.Type.GetType("System.String")));
            comboBox2.DataSource = list_flight_table;
            comboBox2.DisplayMember = "title_state";
            comboBox2.ValueMember = "id_state";
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
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
            GetComboBoxList();
            GetComboBox1();
        }
        private void GetList(int id_train)
        {
            conn.Open();
            string commandStr = "SELECT * FROM flights WHERE id_train = " + id_train.ToString();
            MySqlCommand cmd_get_list = new MySqlCommand(commandStr, conn);
            MySqlDataReader reader_list = cmd_get_list.ExecuteReader();
            while (reader_list.Read())
            {
                string s = "";
                s += "Номер поезда: " + reader_list[0].ToString() + "\n";
                s += "Номер маршрута: " + reader_list[1].ToString() + "\n";
                s += "Цена (руб.): " + reader_list[2].ToString() + " рублей\n";
                s += "Станция отправления: " + reader_list[3].ToString() + "\n";
                s += "Дата отправления: " + reader_list[4].ToString() + "\n";
                s += "Станция прибытия: " + reader_list[5].ToString() + "\n";
                s += "Дата прибытия: " + reader_list[6].ToString() + "\n";
                s += "Статус: " + reader_list[7].ToString();
                MessageBox.Show(s, "Информация");
            }
            reader_list.Close();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dateFlightFromDB;
            string selected_id_flight = comboBox1.SelectedValue.ToString();
            conn.Open();
            string sql = $"SELECT * FROM flights WHERE id_train={selected_id_flight}";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
                textBox3.Text = reader[3].ToString();
                dateFlightFromDB = reader[4].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dateFlightFromDB);
                textBox4.Text = reader[5].ToString();
                dateFlightFromDB = reader[6].ToString();
                dateTimePicker2.Value = Convert.ToDateTime(dateFlightFromDB);
                comboBox2.SelectedValue = reader[7].ToString();
            }
            reader.Close();
            conn.Close();

            int id_selected_train = Convert.ToInt32(comboBox1.SelectedValue);
            GetList(id_selected_train);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text) || String.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Введите все данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string selected_id_flight = comboBox1.SelectedValue.ToString();
                string route_number = textBox1.Text;
                string price = textBox2.Text;
                string departure_station = textBox3.Text;
                DateTime departure_date = dateTimePicker1.Value;
                string arrival_station = textBox4.Text;
                DateTime arrival_date = dateTimePicker2.Value;
                string id_state = comboBox2.SelectedValue.ToString();
                conn.Open();
                string query2 = $"UPDATE flights SET route_number='{route_number}', price='{price}', departure_station='{departure_station}'," +
                    $"departure_date='{departure_date}', arrival_station='{arrival_station}', arrival_date='{arrival_date}', id_state='{id_state}' WHERE id_train = {selected_id_flight}";
                MySqlCommand command = new MySqlCommand(query2, conn);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Изменение прошло успешно.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}