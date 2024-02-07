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
using System.Windows.Forms.DataVisualization.Charting;

namespace Yurt_Otomasyonu
{
    public partial class geliristatistik_uc : UserControl
    {
        public geliristatistik_uc()
        {
            InitializeComponent();
        }

        private decimal toplamgelir()
        {
            decimal totalAmount = 0;

            // SQLite bağlantı dizesi
            string connectionString = "Data Source=database.db;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // SQL sorgusu
                string query = "SELECT SUM(odememiktar) FROM kasa";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalAmount = Convert.ToDecimal(result);
                    }
                }

                connection.Close();
                return totalAmount;
            }
        }

        private void comboboxlist()
        {
            List<string> odemeAylari = new List<string>();

            // SQLite bağlantı dizesi
            string connectionString = "Data Source=database.db;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // SQL sorgusu - Benzersiz "odemeay" değerlerini alır
                string query = "SELECT DISTINCT odemeay FROM kasa";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string odemeAy = reader["odemeay"].ToString();
                            odemeAylari.Add(odemeAy);
                        }
                    }
                }

                connection.Close();
            }

            comboBox1.Items.AddRange(odemeAylari.ToArray());
        }

       private static DataTable GetChartData()
        {
            DataTable dataTable = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=database.db;"))
            {
                connection.Open();

                string query = "SELECT odemeay, SUM(odememiktar) as odememiktar FROM kasa GROUP BY odemeay";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }

                connection.Close();
            }

            return dataTable;
        }
        public static double ToplamOdemeMiktari(string filtre)
        {
            double toplamMiktar = 0;

            // SQLite bağlantı dizesi
            string connectionString = "Data Source=database.db;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // SQL sorgusu
                string query = "SELECT odememiktar FROM kasa WHERE odemeay = @filtre";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@filtre", filtre);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            double miktar = Convert.ToDouble(reader["odememiktar"]);
                            toplamMiktar += miktar;
                        }
                    }
                }

                connection.Close();
            }

            return toplamMiktar;
        }
        private void geliristatistik_uc_Load(object sender, EventArgs e)
        {
            label1.Text = "Kasadaki Para: " + toplamgelir().ToString() + " TL";
            comboboxlist();

            DataTable dataTable = GetChartData();

            // Chart veri bağlama
            chart1.DataSource = dataTable;

            // Grafik türünü belirleme
            chart1.Series.Add("OdemeMiktar");
            chart1.Series["OdemeMiktar"].XValueMember = "odemeay";
            chart1.Series["OdemeMiktar"].YValueMembers = "odememiktar";
            chart1.Series["OdemeMiktar"].ChartType = SeriesChartType.Column;

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            label3.Text = "Seçilen Ay Toplam Para: " + ToplamOdemeMiktari(comboBox1.Text).ToString() + " TL";
        }
    }
}
