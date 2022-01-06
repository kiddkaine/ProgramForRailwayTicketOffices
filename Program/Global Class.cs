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
        public static string ID_TICKET = "0";
        private const string host = "caseum.ru";
        private const string port = "33333";
        private const string database = "db_test";
        private const string username = "test_user";
        private const string password = "test_pass";
        private static readonly MySqlConnection conn = GetDBConnection();
        private static readonly MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private static readonly BindingSource bSource = new BindingSource();
        private static DataSet ds = new DataSet();
        private static DataTable table = new DataTable();

        public static MySqlConnection GetDBConnection()
        {
            string connString = $"server={host};port={port};user={username};database={database};password={password};";
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }

        public static void DeleteUser(string id_stud)
        {
            string sql_delete_user = "DELETE FROM t_stud WHERE id='" + id_stud + "'";
            MySqlCommand delete_user = new MySqlCommand(sql_delete_user, conn);
            conn.Open();
            delete_user.ExecuteNonQuery();
            conn.Close();
        }

        public static void ChangeStateStudent(string new_state, string redact_id)
        {
            conn.Open();
            string query2 = $"UPDATE t_stud SET id_state='{new_state}' WHERE (id='{redact_id}')";
            MySqlCommand command = new MySqlCommand(query2, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static BindingSource GetListUsers()
        {
            conn.Open();
            string commandStr = "SELECT id AS 'Код', fio AS 'ФИО', age AS 'Возраст', theme_kurs AS 'Тема курсовой', id_state AS 'Статус' FROM t_stud";
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
