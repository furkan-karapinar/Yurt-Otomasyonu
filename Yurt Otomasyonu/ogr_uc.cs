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
    public partial class ogr_uc : UserControl
    {
        Database_Control dc = new Database_Control();
        String ogr_columns = "ogr_adi , ogr_soyadi , ogr_tc , ogr_telefon , ogr_dogum_tarihi , ogr_bolum , ogr_mail , ogr_oda_no , ogr_veli_ad_soyadi , ogr_veli_telefon , ogr_veli_adres";

        public ogr_uc()
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
                string query = "SELECT * FROM ogrenciler";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }

                connection.Close();
                dataGridView1.DataSource = dataTable;
            }

            comboBox2.Items.Clear();
            comboBox1.Items.Clear();
            List<string> bolumler = dc.GetBolumAdlari();
            List<string> odalar = dc.GetOdalar();
            comboBox1.Items.AddRange(bolumler.ToArray());
            comboBox2.Items.AddRange(odalar.ToArray());
        }

        private void ogr_uc_Load(object sender, EventArgs e)
        {
          
            update_datagrid();
           
            
        }

       

        public  int GetOdaAktif(string odaNo)
        {
            string connectionString = "Data Source=database.db;";
            int odaAktif = 0;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT oda_aktif FROM odalar WHERE oda_no = @odaNo";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@odaNo", odaNo);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            odaAktif = int.Parse(reader["oda_aktif"].ToString());
                        }
                    }
                }

                connection.Close();
            }

            return odaAktif;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
 if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || maskedTextBox1.Text == string.Empty || maskedTextBox2.Text == string.Empty || maskedTextBox3.Text == string.Empty || comboBox1.Text == string.Empty || 
                textBox3.Text == string.Empty || comboBox2.Text == string.Empty || textBox4.Text == string.Empty || maskedTextBox4.Text == string.Empty || richTextBox1.Text == string.Empty) { MessageBox.Show("Lütfen Boş Alanları Doldurunuz!"); } 
            else
            {
                String ogr_values = ("'" + textBox1.Text + "' , '" + textBox2.Text + "' , '" + maskedTextBox1.Text + "' , '" + maskedTextBox2.Text + "' , '" + maskedTextBox3.Text + "' , '" + comboBox1.Text + "' , '" +
                textBox3.Text + "' , '" + comboBox2.Text + "' , '" + textBox4.Text + "' , '" + maskedTextBox4.Text + "' , '" + richTextBox1.Text + "'");

               int lastid = dc.ReturnLastID_InsertData("ogrenciler", ogr_columns,ogr_values);
                    dc.Insert_Data("borclar","ogrID , ogr_adi , ogr_soyadi , ogr_kalan_borc" , "'" + lastid + "' , '" + textBox1.Text + "' , '" + textBox2.Text + "' , 0");
                   
                    
                    dc.Update_Data("odalar", "oda_no", comboBox2.Text, "oda_aktif", (GetOdaAktif(comboBox2.Text) + 1).ToString());
                    MessageBox.Show("Kayıt Başarılı Bir Şekilde Eklendi!");
            }
            } catch (Exception ex) { MessageBox.Show("Kayıt Hatası"); }

           update_datagrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
       /*ID*/   label13.Text = selectedRow.Cells["ogrID"].Value.ToString();
                textBox1.Text = selectedRow.Cells["ogr_adi"].Value.ToString();
                textBox2.Text = selectedRow.Cells["ogr_soyadi"].Value.ToString(); 
                maskedTextBox1.Text = selectedRow.Cells["ogr_tc"].Value.ToString(); 
                maskedTextBox2.Text = selectedRow.Cells["ogr_telefon"].Value.ToString();
                maskedTextBox3.Text = selectedRow.Cells["ogr_dogum_tarihi"].Value.ToString();
                comboBox1.Text = selectedRow.Cells["ogr_bolum"].Value.ToString();
                textBox3.Text = selectedRow.Cells["ogr_mail"].Value.ToString();
                comboBox2.Text = selectedRow.Cells["ogr_oda_no"].Value.ToString();
                textBox4.Text = selectedRow.Cells["ogr_veli_ad_soyadi"].Value.ToString(); 
                maskedTextBox4.Text = selectedRow.Cells["ogr_veli_telefon"].Value.ToString();
                richTextBox1.Text = selectedRow.Cells["ogr_veli_adres"].Value.ToString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            dc.Update_Data("ogrenciler", "ogrID", label13.Text,"ogr_adi", textBox1.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_soyadi", textBox2.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_tc", maskedTextBox1.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_telefon", maskedTextBox2.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_dogum_tarihi", maskedTextBox3.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_bolum", comboBox1.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_mail", textBox3.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_oda_no", comboBox2.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_veli_ad_soyadi", textBox4.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_veli_telefon", maskedTextBox4.Text);
            dc.Update_Data("ogrenciler", "ogrID", label13.Text, "ogr_veli_adres", richTextBox1.Text);
            dc.Update_Data("borclar", "ogrID", label13.Text, "ogr_adi", textBox1.Text);
            dc.Update_Data("borclar", "ogrID", label13.Text, "ogr_soyadi", textBox2.Text);
                MessageBox.Show("Başarıyla Kaydedildi!");
            } catch { MessageBox.Show("Veri Kaydedilemedi!"); }
            update_datagrid();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dc.Update_Data("odalar", "oda_no", comboBox2.Text, "oda_aktif", (GetOdaAktif(comboBox2.Text) - 1).ToString());
                dc.Delete_Data("ogrenciler", "ogrID", label13.Text);
                dc.Delete_Data("borclar", "ogrID", label13.Text);
                MessageBox.Show("Kayıt Silindi");
            }catch { MessageBox.Show("Veri Hatası"); }
           update_datagrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            maskedTextBox1.Text = string.Empty;
            maskedTextBox2.Text = string.Empty;
            maskedTextBox3.Text = string.Empty;
            comboBox1.Text = string.Empty;
            textBox3.Text = string.Empty;
            comboBox2.Text = string.Empty;
            textBox4.Text = string.Empty;
            maskedTextBox4.Text = string.Empty;
            richTextBox1.Text = string.Empty;

        }
    }
}
