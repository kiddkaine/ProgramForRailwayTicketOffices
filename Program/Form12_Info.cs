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
    public partial class Form12_Info : Form
    {
        MySqlConnection conn;
        public Form12_Info()
        {
            InitializeComponent();
        }
        public void SelectData()
        {
            conn.Open();
            this.Text = $"Отчёт по заказу №{ControlData.ID}";
            string sql = $"SELECT * FROM orders WHERE id_order={ControlData.ID}";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                label1.Text = $"Номер заказа: {reader[0].ToString()}";
                label2.Text = $"Дата заказа: {reader[1].ToString()}";
                label3.Text = $"ID клиента: {reader[2].ToString()}";
                label4.Text = $"Сумма заказа: {reader[3].ToString()} рублей";
            }
            reader.Close();
            conn.Close();
        }
        private void Form12_Info_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";
            conn = new MySqlConnection(connStr);
            SelectData();
        }
    }
}