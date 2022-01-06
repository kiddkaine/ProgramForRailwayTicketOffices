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
    public partial class Form8_Edit : Form
    {
        MySqlConnection conn;
        public Form8_Edit()
        {
            InitializeComponent();
        }
        public void SelectData()
        {
            conn.Open();
            this.Text = $"Редактор поезда №{ControlData.ID_TICKET}";
            string dateFlightFromDB;
            string sql = $"SELECT * FROM flights WHERE id_train={ControlData.ID_TICKET}";
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
                comboBox1.SelectedValue = reader[7].ToString();
            }
            reader.Close();
            conn.Close();
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
        private void Form8_Edit_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            GetComboBox1();
            SelectData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text) || String.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Введите все данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string route_number = textBox1.Text;
                string price = textBox2.Text;
                string departure_station = textBox3.Text;
                DateTime departure_date = dateTimePicker1.Value;
                string arrival_station = textBox4.Text;
                DateTime arrival_date = dateTimePicker2.Value;
                string id_state = comboBox1.SelectedValue.ToString();
                conn.Open();
                string query2 = $"UPDATE flights SET route_number='{route_number}', price='{price}', departure_station='{departure_station}'," +
                    $"departure_date='{departure_date}', arrival_station='{arrival_station}', arrival_date='{arrival_date}', id_state='{id_state}' WHERE id_train = {ControlData.ID_TICKET}";
                MySqlCommand command = new MySqlCommand(query2, conn);
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