using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Yurt_Otomasyonu
{
    public partial class odalar_uc : UserControl
    {
        Database_Control dc = new Database_Control();
        public odalar_uc()
        {
            InitializeComponent();
        }

        private void connect_data()
        {
            string connectionString = "Data Source=database.db;";
            DataTable dataTable = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM odalar";

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
                /*ID*/
                label5.Text = selectedRow.Cells["odaID"].Value.ToString();
                maskedTextBox1.Text = selectedRow.Cells["oda_no"].Value.ToString();
                maskedTextBox2.Text = selectedRow.Cells["oda_kapasite"].Value.ToString();
                maskedTextBox3.Text = selectedRow.Cells["oda_aktif"].Value.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dc.Insert_Data("odalar", "oda_no , oda_kapasite , oda_aktif , oda_durumu", "'" + maskedTextBox1.Text + "' , '" + maskedTextBox2.Text + "' ,'" + maskedTextBox3.Text + "' , '1'");
                MessageBox.Show("Kayıt Başarılı");

            }
            catch { MessageBox.Show("Veri Yazma Hatası"); }
            connect_data();
        }

        private void odalar_uc_Load(object sender, EventArgs e)
        {
            connect_data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dc.Update_Data("odalar", "odaID", label5.Text, "oda_no", maskedTextBox1.Text);
                dc.Update_Data("odalar", "odaID", label5.Text, "oda_kapasite", maskedTextBox2.Text);
                dc.Update_Data("odalar", "odaID", label5.Text, "oda_aktif", maskedTextBox3.Text);
                MessageBox.Show("Kayıt Başarılı");
            }
            catch { MessageBox.Show("Veri Yazma Hatası"); }

            connect_data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dc.Delete_Data("odalar", "odaID", label5.Text);

            }
            catch { MessageBox.Show("Veri Yazma Hatası"); }
            connect_data();
        }
    }
}

