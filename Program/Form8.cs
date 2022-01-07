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
    public partial class Form8 : Form
    {
        MySqlConnection conn;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private BindingSource bSource = new BindingSource();
        private DataSet ds = new DataSet();
        private DataTable table = new DataTable();
        string id_selected_rows = "0";
        public Form8()
        {
            InitializeComponent();
        }
        public void GetSelectedIDString()
        {
            string index_selected_rows;
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            toolStripLabel1.Text = id_selected_rows;
        }
        public void DeleteFlights()
        {
            string sql_delete_flight = "DELETE FROM flights WHERE id_train='" + id_selected_rows + "'";
            MySqlCommand delete_flight = new MySqlCommand(sql_delete_flight, conn);
            try
            {
                conn.Open();
                delete_flight.ExecuteNonQuery();
                MessageBox.Show("Удаление прошло успешно!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления строки.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn.Close();
                reload_list();
            }
        }
        public void ChangeStateFlights(string new_state)
        {
            string redact_id = id_selected_rows;
            conn.Open();
            string query2 = $"UPDATE flights SET id_state='{new_state}' WHERE (id_train='{redact_id}')";
            MySqlCommand command = new MySqlCommand(query2, conn);
            command.ExecuteNonQuery();
            conn.Close();
            reload_list();
        }
        public void GetListFlights()
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
            int count_rows = dataGridView1.RowCount - 1;
            toolStripLabel2.Text = (count_rows).ToString();
        }
        public void reload_list()
        {
            table.Clear();
            GetListFlights();
            ChangeColorDGV();
        }
        private void ChangeColorDGV()
        {
            int count_rows = dataGridView1.RowCount - 1;
            toolStripLabel1.Text = (count_rows).ToString();
            for (int i = 0; i < count_rows; i++)
            {
                int id_selected_status = Convert.ToInt32(dataGridView1.Rows[i].Cells[7].Value);
                if (id_selected_status == 1)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
                if (id_selected_status == 2)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }
        private void Form8_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";

            conn = new MySqlConnection(connStr);

            GetListFlights();

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

            ChangeColorDGV();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ChangeStateFlights("1");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ChangeStateFlights("2");
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DeleteFlights();
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            reload_list();
            ChangeColorDGV();
            toolStripTextBox1.Text = "";
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

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
            {
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                dataGridView1.CurrentCell.Selected = true;
                GetSelectedIDString();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Form8_Add Form8_Add = new Form8_Add();
            Form8_Add.ShowDialog();
            reload_list();
            ChangeColorDGV();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            bSource.Filter = "[Номер маршрута] LIKE'" + toolStripTextBox1.Text + "%'";
            reload_list();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ControlData.ID = id_selected_rows;
            Form8_Edit Form8_Edit = new Form8_Edit();
            Form8_Edit.ShowDialog();
            reload_list();
        }
    }
}