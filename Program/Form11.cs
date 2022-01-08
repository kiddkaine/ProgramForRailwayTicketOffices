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
    public partial class Form11 : Form
    {
        MySqlConnection conn;

        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private BindingSource bSource = new BindingSource();
        private DataSet ds = new DataSet();
        private DataTable table = new DataTable();

        string id_selected_rows = "0";
        string id_selected_clients = "0";
        string titleItems_selected_rows = "";
        string priceItems_selected_rows = "";
        bool issetOrder = false;
        double sum_order = 0;

        public void GetComboBox1()
        {
            DataTable list_client_table = new DataTable();
            MySqlCommand list_client_command = new MySqlCommand();
            conn.Open();
            list_client_table.Columns.Add(new DataColumn("id_client", System.Type.GetType("System.Int32")));
            list_client_table.Columns.Add(new DataColumn("fio_client", System.Type.GetType("System.String")));
            comboBox1.DataSource = list_client_table;
            comboBox1.DisplayMember = "fio_client";
            comboBox1.ValueMember = "id_client";
            string sql_list_users = $"SELECT id_client, fio_client FROM clients";
            list_client_command.CommandText = sql_list_users;
            list_client_command.Connection = conn;
            MySqlDataReader list_client_reader;
            try
            {
                list_client_reader = list_client_command.ExecuteReader();
                while (list_client_reader.Read())
                {
                    DataRow rowToAdd = list_client_table.NewRow();
                    rowToAdd["id_client"] = Convert.ToInt32(list_client_reader[0]);
                    rowToAdd["fio_client"] = list_client_reader[1].ToString();
                    list_client_table.Rows.Add(rowToAdd);
                }
                list_client_reader.Close();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn.Close();
            }
        }
        public void GetFirstListFlights()
        {
            string commandStr = "SELECT id_train AS 'Номер поезда', route_number AS 'Номер маршрута', price AS 'Цена (руб.)', " +
                "departure_station AS 'Станция отправления', departure_date AS 'Дата отправления', " +
                "arrival_station AS 'Станция прибытия', arrival_date AS 'Дата прибытия', id_state AS 'Статус' FROM flights";
            conn.Open();
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            MyDA.Fill(table);
            bSource.DataSource = table;
            dataGridView1.DataSource = bSource;
            conn.Close();
        }
        public void GetSelectedIDString()
        {
            string index_selected_rows;
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            titleItems_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[1].Value.ToString();
            priceItems_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[2].Value.ToString();

        }
        public void InsertOrderMain()
        {
            string date_order = DateTime.Now.ToString();
            string id_client = id_selected_clients;
            string sum_order = "0";

            string sql_update_current_order = $"INSERT INTO orders (date_order, id_client, sum_order) " +
                                              $"VALUES ('{date_order}', '{id_client}', '{sum_order}'); " +
                                              $"SELECT id_order FROM orders WHERE (id_order = LAST_INSERT_ID());";
            conn.Open();
            MySqlCommand command = new MySqlCommand(sql_update_current_order, conn);
            string new_inserted_order_id = command.ExecuteScalar().ToString();
            SomeClass.new_inserted_order_id = new_inserted_order_id;
            toolStripStatusLabel1.Text = $"Добавлен заказ с ID: {SomeClass.new_inserted_order_id}";
            conn.Close();
        }
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";

            conn = new MySqlConnection(connStr);

            GetFirstListFlights();

            GetComboBox1();
            comboBox1.Text = "";

            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
            dataGridView1.Columns[5].Visible = true;
            dataGridView1.Columns[6].Visible = true;
            dataGridView1.Columns[7].Visible = true;

            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 10;
            dataGridView1.Columns[2].FillWeight = 10;
            dataGridView1.Columns[3].FillWeight = 15;
            dataGridView1.Columns[4].FillWeight = 15;
            dataGridView1.Columns[5].FillWeight = 15;
            dataGridView1.Columns[6].FillWeight = 15;
            dataGridView1.Columns[7].FillWeight = 10;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[7].ReadOnly = true;

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.ColumnHeadersVisible = true;

            dataGridView1.AllowUserToResizeColumns = false;

            dataGridView1.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.MultiSelect = false;

            bSource.Filter = "[Статус] = " + 1;

        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
            {
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                dataGridView1.CurrentCell.Selected = true;
                GetSelectedIDString();
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                dataGridView1.CurrentRow.Selected = true;
                GetSelectedIDString();
            }
            catch
            {

            }
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            sum_order = 0;
            int selRowNum = dataGridView2.CurrentCell.RowIndex;
            double Position = Convert.ToDouble(dataGridView2.Rows[selRowNum].Cells[3].Value) * Convert.ToDouble(dataGridView2.Rows[selRowNum].Cells[2].Value);
            dataGridView2.Rows[selRowNum].Cells[4].Value = Position.ToString();
            int count_position = dataGridView2.Rows.Count;
            for (int i = 0; i < count_position; i++)
            {
                string count_ticket = dataGridView2.Rows[i].Cells[2].Value.ToString();
                double price_ticket = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                sum_order += Convert.ToInt32(count_ticket) * price_ticket;
            }
            label1.Text = $"Итоговая сумма: {sum_order.ToString()} рублей";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowNumber = dataGridView2.Rows.Add();
            dataGridView2.Rows[rowNumber].Cells[0].Value = id_selected_rows;
            dataGridView2.Rows[rowNumber].Cells[1].Value = titleItems_selected_rows;
            dataGridView2.Rows[rowNumber].Cells[2].Value = "1";
            dataGridView2.Rows[rowNumber].Cells[3].Value = priceItems_selected_rows;
            if (label7.Text == "1")
            {
                dataGridView2.Rows[rowNumber].Cells[4].Value = Convert.ToInt32(priceItems_selected_rows) / 2;
            }
            if (label7.Text == "2")
            {
                dataGridView2.Rows[rowNumber].Cells[4].Value = priceItems_selected_rows;
            }
            try
            {
                sum_order += Convert.ToDouble(dataGridView2.Rows[rowNumber].Cells[4].Value) * Convert.ToDouble(dataGridView2.Rows[rowNumber].Cells[2].Value);
                label1.Text = $"Итоговая сумма: {sum_order.ToString()} рублей";
            }
            catch
            {

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form11_AddClient Form11_AddClient = new Form11_AddClient();
            Form11_AddClient.ShowDialog();
            GetComboBox1();
            comboBox1.SelectedValue = Convert.ToInt32(SomeClass.new_inserted_id);
            toolStripStatusLabel1.Text = $"Добавлен клиент с ID: {SomeClass.new_inserted_id}";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Выберите клиента!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                id_selected_clients = comboBox1.SelectedValue.ToString();
                InsertOrderMain();
                issetOrder = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (issetOrder)
            {
                double sumOrder = 0;
                int countPosition = dataGridView2.Rows.Count;
                conn.Open();
                for (int i = 0; i < countPosition; i++)
                {
                    string idItems = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    string countItems = dataGridView2.Rows[i].Cells[2].Value.ToString();
                    double priceItems = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                    string idOrder = SomeClass.new_inserted_order_id;
                    sumOrder += Convert.ToInt32(countItems) * priceItems;
                    string query = $"INSERT INTO ticket_orders (id_train, quantity_ticket, id_order) " +
                        $"VALUES ('{idItems}', '{countItems}', {idOrder})";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                }
                conn.Close();

                if (label7.Text == "1")
                {
                    toolStripStatusLabel1.Text = $"Итоговая сумма заказа №{SomeClass.new_inserted_order_id} составляет {sumOrder / 2} рублей";
                    conn.Open();
                    string query2 = $"UPDATE orders SET sum_order='{sumOrder / 2}' WHERE (id_order='{SomeClass.new_inserted_order_id}')";
                    MySqlCommand comman1 = new MySqlCommand(query2, conn);
                    comman1.ExecuteNonQuery();
                    conn.Close();
                    dataGridView2.Rows.Clear();
                    SomeClass.new_inserted_order_id = "0";
                }
                if (label7.Text == "2")
                {
                    toolStripStatusLabel1.Text = $"Итоговая сумма заказа №{SomeClass.new_inserted_order_id} составляет {sumOrder} рублей";
                    conn.Open();
                    string query2 = $"UPDATE orders SET sum_order='{sumOrder}' WHERE (id_order='{SomeClass.new_inserted_order_id}')";
                    MySqlCommand comman1 = new MySqlCommand(query2, conn);
                    comman1.ExecuteNonQuery();
                    conn.Close();
                    dataGridView2.Rows.Clear();
                    SomeClass.new_inserted_order_id = "0";
                }
            }
            else
            {
                MessageBox.Show("Заказ не создан. Создайте заказ!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bSource.Filter = "[Станция прибытия] LIKE'" + textBox1.Text + "%'";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_id_flight = comboBox1.SelectedValue.ToString();
            conn.Open();
            string sql = $"SELECT id_privilege FROM clients WHERE id_client={selected_id_flight}";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                label7.Text = reader[0].ToString();
            }
            reader.Close();
            conn.Close();
        }
    }
}