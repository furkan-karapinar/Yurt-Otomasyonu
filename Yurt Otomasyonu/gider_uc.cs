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
    public partial class gider_uc : UserControl
    {
        Database_Control dc = new Database_Control();
        public gider_uc()
        {
            InitializeComponent();
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
                string query = "SELECT * FROM giderler";

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
                dc.Insert_Data("giderler","elektrik , su , dogalgaz , internet , gida , personel , diger" , "'" + textBox1.Text + "' , '" + textBox2.Text + "' , '" + textBox3.Text + "' , '" + textBox4.Text + "' , '" + textBox5.Text + "' , '" + textBox6.Text + "' , '" + textBox7.Text + "'");
                MessageBox.Show("Başarıyla Kaydedildi!");
            } catch (Exception ex) { MessageBox.Show("Kayıt Hatası!"); }
            update_datagrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Geçerli bir satırın seçildiğinden emin olun
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
               label9.Text = selectedRow.Cells["odemeID"].Value.ToString();

                textBox1.Text = selectedRow.Cells["elektrik"].Value.ToString();
                textBox2.Text = selectedRow.Cells["su"].Value.ToString();
                textBox3.Text = selectedRow.Cells["dogalgaz"].Value.ToString();
                textBox4.Text = selectedRow.Cells["internet"].Value.ToString();
                textBox5.Text = selectedRow.Cells["gida"].Value.ToString();
                textBox6.Text = selectedRow.Cells["personel"].Value.ToString();
                textBox7.Text = selectedRow.Cells["diger"].Value.ToString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            dc.Update_Data("giderler","odemeID",label9.Text,"elektrik",textBox1.Text);
            dc.Update_Data("giderler", "odemeID", label9.Text, "su", textBox2.Text);
            dc.Update_Data("giderler", "odemeID", label9.Text, "dogalgaz", textBox3.Text);
            dc.Update_Data("giderler", "odemeID", label9.Text, "internet", textBox4.Text);
            dc.Update_Data("giderler", "odemeID", label9.Text, "gida", textBox5.Text);
            dc.Update_Data("giderler", "odemeID", label9.Text, "personel", textBox6.Text);
            dc.Update_Data("giderler", "odemeID", label9.Text, "diger", textBox7.Text);
            MessageBox.Show("Başarıyla Kaydedildi!");
            }catch { MessageBox.Show("Kayıt Hatası!"); }
            update_datagrid(); 

           
        }

        private void gider_uc_Load(object sender, EventArgs e)
        {
            update_datagrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dc.Delete_Data("giderler","odemeID",label9.Text);
            update_datagrid();
        }
    }
}
