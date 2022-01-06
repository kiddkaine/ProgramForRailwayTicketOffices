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
    public partial class Form9 : Form
    {
        MySqlConnection conn;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private BindingSource bSource = new BindingSource();
        private DataSet ds = new DataSet();
        private DataTable table = new DataTable();
        string id_selected_rows = "0";
        public Form9()
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
        public void DeleteClients()
        {
            string sql_delete_client = "DELETE FROM clients WHERE id_client='" + id_selected_rows + "'";
            MySqlCommand delete_client = new MySqlCommand(sql_delete_client, conn);
            try
            {
                conn.Open();
                delete_client.ExecuteNonQuery();
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
        public void ChangeStateClients(string new_privilege)
        {
            string redact_id = id_selected_rows;
            conn.Open();
            string query2 = $"UPDATE clients SET id_privilege='{new_privilege}' WHERE (id_client='{redact_id}')";
            MySqlCommand command = new MySqlCommand(query2, conn);
            command.ExecuteNonQuery();
            conn.Close();
            reload_list();
        }
        public void GetListClients()
        {
            string commandStr = "SELECT id_client AS 'ID клиента', fio_client AS 'Имя клиента', pass_client AS 'Паспорт', " +
                "id_privilege AS 'Уровень льготы' FROM clients";
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
            GetListClients();
            ChangeColorDGV();
        }
        private void ChangeColorDGV()
        {
            int count_rows = dataGridView1.RowCount - 1;
            toolStripLabel1.Text = (count_rows).ToString();
            for (int i = 0; i < count_rows; i++)
            {
                int id_selected_status = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                if (id_selected_status == 1)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                if (id_selected_status == 2)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_3_10_19;database=st_3_10_19;password=73161475;";

            conn = new MySqlConnection(connStr);

            GetListClients();

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

            ChangeColorDGV();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ChangeStateClients("1");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ChangeStateClients("2");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DeleteClients();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Form9_Add Form9_Add = new Form9_Add();
            Form9_Add.ShowDialog();
            reload_list();
            ChangeColorDGV();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
            reload_list();
            ChangeColorDGV();
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
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
            Form9_Edit Form9_Edit = new Form9_Edit();
            Form9_Edit.ShowDialog();
            reload_list();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            bSource.Filter = "[Имя клиента] LIKE'" + toolStripTextBox1.Text + "%'";
            reload_list();
        }
    }
}