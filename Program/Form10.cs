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
    public partial class Form10 : Form
    {
        MySqlConnection conn;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private BindingSource bSource = new BindingSource();
        private DataSet ds = new DataSet();
        private DataTable table = new DataTable();
        string id_selected_rows = "0";
        public Form10()
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
        public void DeleteEmployees()
        {
            string sql_delete_employee = "DELETE FROM employees WHERE id_employee='" + id_selected_rows + "'";
            MySqlCommand delete_employee = new MySqlCommand(sql_delete_employee, conn);
            try
            {
                conn.Open();
                delete_employee.ExecuteNonQuery();
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
        public void GetListEmployees()
        {
            string commandStr = "SELECT id_employee AS 'ID сотрудника', fio_employee AS 'Имя сотрудника', post_employee AS 'Должность' " +
                " FROM employees";
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
            GetListEmployees();
        }
        private void Form10_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";

            conn = new MySqlConnection(connStr);

            GetListEmployees();

            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;

            dataGridView1.Columns[0].FillWeight = 20;
            dataGridView1.Columns[1].FillWeight = 40;
            dataGridView1.Columns[2].FillWeight = 40;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.ColumnHeadersVisible = true;

            dataGridView1.AllowUserToResizeColumns = false;

            dataGridView1.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.MultiSelect = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DeleteEmployees();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form10_Add Form10_Add = new Form10_Add();
            Form10_Add.ShowDialog();
            reload_list();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
            reload_list();
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Close();
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ControlData.ID_TICKET = id_selected_rows;
            Form10_Edit Form10_Ticket= new Form10_Edit();
            Form10_Ticket.ShowDialog();
            reload_list();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            bSource.Filter = "[Имя сотрудника] LIKE'" + toolStripTextBox1.Text + "%'";
            reload_list();
        }
    }
}