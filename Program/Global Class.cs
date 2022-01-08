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
    class ControlData
    {
        public static string ID = "0";
        private const string host = "caseum.ru";
        private const string port = "33333";
        private const string database = "st_3_10_19";
        private const string username = "st_3_10_19";
        private const string password = "73161475";
        private static readonly MySqlConnection conn = ConnDB();
        private static readonly MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private static readonly BindingSource bSource = new BindingSource();
        private static DataSet ds = new DataSet();
        private static DataTable table = new DataTable();


        public static MySqlConnection ConnDB()
        {
            string connString = $"server={host};port={port};user={username};database={database};password={password};";
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }

        public static void DeleteOrders(string id_order)
        {
            string sql_delete_order = "DELETE FROM orders WHERE id_order='" + id_order + "'";
            MySqlCommand delete_order = new MySqlCommand(sql_delete_order, conn);
            conn.Open();
            delete_order.ExecuteNonQuery();
            conn.Close();
        }

        public static BindingSource GetListOrders()
        {
            conn.Open();
            string commandStr = "SELECT id_order AS 'Номер заказа', date_order AS 'Дата заказа', id_client AS 'ID клиента', sum_order AS 'Сумма заказа (руб.)' FROM orders";
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            MyDA.Fill(table);
            bSource.DataSource = table;
            conn.Close();
            return bSource;
        }

        public static void ReloadList()
        {
            table.Clear();
        }
    }
}