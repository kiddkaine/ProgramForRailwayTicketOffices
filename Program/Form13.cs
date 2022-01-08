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
    public partial class Form13 : Form
    {
        MySqlConnection conn;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private BindingSource bSource = new BindingSource();
        private DataSet ds = new DataSet();
        private DataTable table = new DataTable();
        string id_selected_rows = "0";
        public Form13()
        {
            InitializeComponent();
        }
        public void GetSelectedIDString()
        {
            string index_selected_rows;
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            toolStripLabel5.Text = id_selected_rows;
        }
        public void DeleteTickets()
        {
            string sql_delete_flight = "DELETE FROM ticket_orders WHERE id_ticket='" + id_selected_rows + "'";
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
        public void GetListTickets()
        {
            string commandStr = "SELECT id_ticket AS 'ID билета', id_train AS 'Номер поезда', quantity_ticket AS 'Количество билетов', id_order AS 'ID заказа' FROM ticket_orders";
            conn.Open();
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            MyDA.Fill(table);
            bSource.DataSource = table;
            dataGridView1.DataSource = bSource;
            conn.Close();
            int count_rows = dataGridView1.RowCount - 1;
            toolStripLabel7.Text = (count_rows).ToString();
        }
        public void reload_list()
        {
            table.Clear();

            GetListTickets();
        }
        private void Form13_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";

            conn = new MySqlConnection(connStr);

            GetListTickets();

            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;

            dataGridView1.Columns[0].FillWeight = 15;
            dataGridView1.Columns[1].FillWeight = 35;
            dataGridView1.Columns[2].FillWeight = 25;
            dataGridView1.Columns[3].FillWeight = 25;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.ColumnHeadersVisible = true;

            dataGridView1.AllowUserToResizeColumns = false;

            dataGridView1.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.MultiSelect = false;

            int count_rows = dataGridView1.RowCount - 1;
            toolStripLabel7.Text = (count_rows).ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            reload_list();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DeleteTickets();
            reload_list();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Печать успешно выполнена!", "Печать", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ControlData.ID = id_selected_rows;
            Form13_Info Form13_Info = new Form13_Info();
            Form13_Info.ShowDialog();
            reload_list();
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
    }
}