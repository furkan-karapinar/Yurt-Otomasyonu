using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yurt_Otomasyonu
{
    public partial class bolum_uc : UserControl
    {
        Database_Control dc = new Database_Control();
        public bolum_uc()
        {
            InitializeComponent();
        }

        private void bolum_uc_Load(object sender, EventArgs e)
        {
            update_datagrid();
        }

        private void update_datagrid() 
        {
            DataTable dataTable = new DataTable();

            // SQLite bağlantı dizesi
            string connectionString = "Data Source=database.db;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // SQL sorgusu
                string query = "SELECT * FROM bolumler";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }

                connection.Close();
                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Geçerli bir satırın seçildiğinden emin olun
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string bolum_adi = selectedRow.Cells["bolum_adi"].Value.ToString();
                string idValue = selectedRow.Cells["bolumID"].Value.ToString();
                textBox1.Text = bolum_adi;
                label3.Text = idValue;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dc.Update_Data("bolumler","bolumID",label3.Text,"bolum_adi",textBox1.Text);
            update_datagrid() ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dc.Delete_Data("bolumler","bolumID", label3.Text);
            update_datagrid() ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dc.Insert_Data("bolumler","bolum_adi","'" + textBox1.Text + "'");
            update_datagrid() ;
        }
    }
}
