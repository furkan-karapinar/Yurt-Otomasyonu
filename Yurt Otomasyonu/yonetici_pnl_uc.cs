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
    public partial class yonetici_pnl_uc : UserControl
    {
        Database_Control dc = new Database_Control();

        public yonetici_pnl_uc()
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

                string query = "SELECT * FROM yonetici";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }

                connection.Close();
                dataGridView1.DataSource = dataTable;
            }

        }

        private void yonetici_pnl_uc_Load(object sender, EventArgs e)
        {
            connect_data();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string yoneticiID = selectedRow.Cells["yoneticiID"].Value.ToString();
                label4.Text = yoneticiID;
                textBox1.Text = selectedRow.Cells["yonetici_adi"].Value.ToString();
               textBox2.Text = selectedRow.Cells["yonetici_sifre"].Value.ToString();
            }
            } catch { }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
dc.Insert_Data("yonetici","yonetici_adi , yonetici_sifre" , "'" + textBox1.Text + "' , '" + textBox2.Text + "'");
                MessageBox.Show("Kayıt Başarılı");
               
            }catch { MessageBox.Show("Veri Yazma Hatası"); }
             connect_data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
 dc.Delete_Data("yonetici", "yoneticiID", label4.Text);
              
            } catch { MessageBox.Show("Veri Yazma Hatası"); }
           connect_data() ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
 dc.Update_Data("yonetici", "yoneticiID", label4.Text, "yonetici_adi", textBox1.Text);
            dc.Update_Data("yonetici", "yoneticiID", label4.Text, "yonetici_sifre", textBox2.Text);
                MessageBox.Show("Kayıt Başarılı");
            } catch { MessageBox.Show("Veri Yazma Hatası"); }

            connect_data();
        }
    }
}
