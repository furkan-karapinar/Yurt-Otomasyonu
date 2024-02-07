using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Yurt_Otomasyonu
{
    internal class Database_Control
    {
        // Veritabanı adı ve konumu
        string path = "database.db", cs = @"URI=file:" + Application.StartupPath + "\\database.db";

        // Gerekli tanımlamalar
        SQLiteConnection data_connection;
        SQLiteCommand command;
        SQLiteDataReader reader;



        public void Create_Database(String datatable_name, String data_options)
        {
            // Veritablosu yoksa oluşturulur. Varsa oluşturmaz. Hata durumunda kullanıcıya belirtilir.
            try
            {
                // Veritabanı var mı sorgulama
                if (!System.IO.File.Exists(path))
                {
                    // if sorgusunda '!' işareti mevcut olduğundan veritabanı yoktur. Bu yüzden veritabanı dosyası oluşturulur.
                    SQLiteConnection.CreateFile(path);
                }

                // İstenilen veritablosu yoksa oluşturulur.
                using (var sqlite = new SQLiteConnection(@"Data Source=" + path))
                {
                    sqlite.Open();
                    string sql = "CREATE TABLE IF NOT EXISTS " + datatable_name + " (" + data_options + ")";
                    SQLiteCommand cmd = new SQLiteCommand(sql, sqlite);
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
            catch { MessageBox.Show("Veritabanı Oluşturma Hatası"); }

        }

        public List<string> GetOdalar()
        {
            List<string> odaNumaralari = new List<string>();

            // SQLite bağlantı dizesi
            string connectionString = "Data Source=database.db;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // SQL sorgusu
                string query = "SELECT oda_no, oda_kapasite, oda_aktif FROM odalar";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string odaNo = reader["oda_no"].ToString();
                            string odaKapasite = reader["oda_kapasite"].ToString();
                            string odaAktif = reader["oda_aktif"].ToString();

                            if (odaKapasite != odaAktif)
                            {
                                odaNumaralari.Add(odaNo);
                            }
                        }
                    }
                }

                connection.Close();
            }

            return odaNumaralari;
        }

        public List<string> GetBolumAdlari()
        {
            List<string> bolumAdlari = new List<string>();

            // SQLite bağlantı dizesi
            string connectionString = "Data Source=database.db;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // SQL sorgusu
                string query = "SELECT bolum_adi FROM bolumler";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string bolumAdi = reader["bolum_adi"].ToString();
                            bolumAdlari.Add(bolumAdi);
                        }
                    }
                }

                connection.Close();
            }

            return bolumAdlari;
        }
     
        public void Delete_Data(String datatable_name, String database_item_name, String item_name)
        {
            // Burası (Genel) veritablosundan veri silme yeridir. Hata durumunda kullanıcıya belirtilir.
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "DELETE FROM " + datatable_name + " WHERE " + database_item_name + "=@name";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", item_name);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            catch { MessageBox.Show("Veri Silme Hatası"); }
        }

        public void Insert_Data(String datatable_name, String item_names, String item_values)
        {
            // Burası veritablosuna birden fazla veri işlemek içindir.
            // Hata durumunda ayrıca belirtilir.
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO " + datatable_name + "(" + item_names + ") VALUES(" + item_values + ")";
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            catch { MessageBox.Show("Veri Giriş Hatası"); }


        }

        public int ReturnLastID_InsertData(string datatable_name, string item_names, string item_values)
        {
            int lastInsertedId = -1;

            try
            {
                using (var con = new SQLiteConnection(cs))
                {
                    con.Open();
                    using (var cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "INSERT INTO " + datatable_name + "(" + item_names + ") VALUES(" + item_values + ")";
                        cmd.ExecuteNonQuery();

                        // Son eklenen öğenin ID değerini almak için "last_insert_rowid()" fonksiyonunu kullanıyoruz.
                        cmd.CommandText = "SELECT last_insert_rowid()";
                        lastInsertedId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Veri Giriş Hatası");
            }

            return lastInsertedId;
        }

        public void Update_Data(String datatable_name, String where_column_name , String where_column_value, String item_name, String item_value)
        {
            // Burası ayarların verilerini güncellemek içindir. Hata durumunda belirtilir.
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "UPDATE " + datatable_name + " SET " + item_name + "=@value WHERE " + where_column_name + "=@name";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", where_column_value);
                cmd.Parameters.AddWithValue("@value", item_value);
                cmd.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Veri Değiştirme Hatası"); }


        }
        

    }
}
