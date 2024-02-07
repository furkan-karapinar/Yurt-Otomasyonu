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
    public partial class personel_uc : UserControl
    {
        Database_Control dc = new Database_Control();
        public personel_uc()
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

                string query = "SELECT * FROM personeller";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }

                connection.Close();
                dataGridView1.DataSource = dataTable;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dc.Insert_Data("personeller", "personel_ad_soyadi , personel_departmani", "'" + textBox1.Text + "' , '" + textBox2.Text + "'");
                MessageBox.Show("Kayıt Başarılı");

            }
            catch { MessageBox.Show("Veri Yazma Hatası"); }
            connect_data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dc.Delete_Data("personeller", "personelID", label4.Text);
                
            }
            catch { MessageBox.Show("Veri Yazma Hatası"); }
            connect_data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dc.Update_Data("personeller", "personelID", label4.Text, "personel_ad_soyadi", textBox1.Text);
                dc.Update_Data("personeller", "personelID", label4.Text, "personel_departmani", textBox2.Text);
                MessageBox.Show("Kayıt Başarılı");
            }
            catch { MessageBox.Show("Veri Yazma Hatası"); }

            connect_data();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    string yoneticiID = selectedRow.Cells["personelID"].Value.ToString();
                    label4.Text = yoneticiID;
                    textBox1.Text = selectedRow.Cells["personel_ad_soyadi"].Value.ToString();
                    textBox2.Text = selectedRow.Cells["personel_departmani"].Value.ToString();
                }
            }
            catch { }
        }

        private void personel_uc_Load(object sender, EventArgs e)
        {
            connect_data();
        }
    }
}
