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
    public partial class odemeler_uc : UserControl
    {
        Database_Control dc = new Database_Control();
        public odemeler_uc()
        {
            InitializeComponent();
        }

        private void odemeler_uc_Load(object sender, EventArgs e)
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
                string query = "SELECT * FROM borclar";

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
                textBox1.Text  = selectedRow.Cells["ogr_adi"].Value.ToString();
                textBox2.Text = selectedRow.Cells["ogr_soyadi"].Value.ToString();
                textBox4.Text = selectedRow.Cells["ogr_kalan_borc"].Value.ToString();
                label5.Text = selectedRow.Cells["ogrID"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int odenen, kalan, yeniborc;
            odenen = Convert.ToInt16(textBox3.Text);

            kalan = Convert.ToInt16(textBox4.Text);
            yeniborc = kalan - odenen;
            textBox4.Text = yeniborc.ToString();

            dc.Update_Data("borclar","ogrID",label5.Text,"ogr_kalan_borc",textBox4.Text);

            dc.Insert_Data("kasa", "odemeay , odememiktar", "'" + textBox5.Text + "' , '" + textBox4.Text + "'");

            update_datagrid();
        }
        static void UpdateOgrKalanBorc(string borc)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=database.db;"))
            {
                connection.Open();

                string query = "UPDATE borclar SET ogr_kalan_borc = '" + borc + "'";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dc.Update_Data("borclar", "ogrID", label5.Text, "ogr_kalan_borc", textBox6.Text);
            MessageBox.Show("Seçilene Uygulandı");
            update_datagrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateOgrKalanBorc(textBox6.Text);
            MessageBox.Show("Tümüne Uygulandı");
            update_datagrid();
        }
    }
}
