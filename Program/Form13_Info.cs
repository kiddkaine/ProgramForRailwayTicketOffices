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
    public partial class Form13_Info : Form
    {
        MySqlConnection conn;
        public Form13_Info()
        {
            InitializeComponent();
        }
        public void SelectData()
        {
            conn.Open();
            this.Text = $"Информация о билете №{ControlData.ID}";
            string sql = $"SELECT * FROM ticket_orders WHERE id_ticket={ControlData.ID}";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                label1.Text = $"ID билета: {reader[0].ToString()}";
                label2.Text = $"Номер поезда: {reader[1].ToString()}";
                label3.Text = $"Количество билетов: {reader[2].ToString()} шт.";
                label4.Text = $"ID заказа: {reader[3].ToString()}";
            }
            reader.Close();
            conn.Close();
        }
        private void Form13_Info_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            SelectData();
        }
    }
}