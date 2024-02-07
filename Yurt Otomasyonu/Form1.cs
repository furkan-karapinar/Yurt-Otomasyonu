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
    public partial class Form1 : Form
    {
        Database_Control dc = new Database_Control();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //dc.Create_Database("ogrenciler", "ogrID INTEGER PRIMARY KEY , ogr_adi VARCHAR , ogr_soyadi VARCHAR , ogr_tc VARCHAR , ogr_telefon VARCHAR , ogr_dogum_tarihi VARCHAR , ogr_mail VARCHAR , ogr_oda_no VARCHAR , ogr_veli_ad_soyadi VARCHAR , ogr_veli_telefon VARCHAR , ogr_veli_adres VARCHAR");
            //dc.Create_Database("bolumler","bolumID INTEGER PRIMARY KEY , bolum_adi VARCHAR");
            //dc.Create_Database("odalar", "odaID INTEGER PRIMARY KEY , oda_no VARCHAR , oda_kapasite VARCHAR , oda_aktif VARCHAR , oda_durumu VARCHAR");
            //dc.Create_Database("yonetici", "yoneticiID INTEGER PRIMARY KEY , yonetici_adi VARCHAR , yonetici_sifre VARCHAR");
            //dc.Create_Database("borclar", "ogrID INTEGER , ogr_adi VARCHAR , ogr_soyadi VARCHAR , ogr_kalan_borc VARCHAR");
            //dc.Create_Database("personeller" , "personelID INTEGER PRIMARY KEY , personel_ad_soyadi VARCHAR , personel_departmani VARCHAR");
            //dc.Create_Database("giderler" , "odemeID INTEGER PRIMARY KEY , elektrik INTEGER , su INTEGER , dogalgaz INTEGER , internet INTEGER , gida INTEGER , personel INTEGER , diger INTEGER");

            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;

        }
        public static bool CheckLogin(string yoneticiAdi, string sifre)
        {
            string connectionString = "Data Source=database.db;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT yonetici_sifre FROM yonetici WHERE yonetici_adi = @yoneticiAdi";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@yoneticiAdi", yoneticiAdi);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedSifre = reader["yonetici_sifre"].ToString();
                            if (sifre == storedSifre)
                            {
                                return true;
                            }
                        }
                    }
                }

                connection.Close();
            }

            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool checklgn = CheckLogin(textBox1.Text, textBox2.Text);
            if (checklgn)
            {
                MessageBox.Show("Giriş Yapıldı!");
                MessageBox.Show("Üst Panel Kilidi Açıldı!");
                menuStrip1.Enabled = true;
                container_pnl.Controls.Clear();
                
            }
            else
            {
                MessageBox.Show("Hatalı");
                menuStrip1.Enabled = false;
            }
        }

        private void öğrencilerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            container_pnl.Controls.Clear();
            ogr_uc ogr_Uc = new ogr_uc();
            ogr_Uc.Dock = DockStyle.Fill;
            container_pnl.Controls.Add(ogr_Uc);
           

            öğrencilerToolStripMenuItem.BackColor = Color.LightSkyBlue;
            bölümlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            borçlarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            giderlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            personellerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            odalarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            kasaToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            yöneticilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void bölümlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            container_pnl.Controls.Clear();
            bolum_uc ogr_Uc = new bolum_uc();
            ogr_Uc.Dock = DockStyle.Fill;
            container_pnl.Controls.Add(ogr_Uc);

            öğrencilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            bölümlerToolStripMenuItem.BackColor = Color.LightSkyBlue;
            borçlarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            giderlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            personellerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            odalarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            kasaToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            yöneticilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void borçlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            container_pnl.Controls.Clear();
            odemeler_uc ogr_Uc = new odemeler_uc();
            ogr_Uc.Dock = DockStyle.Fill;
            container_pnl.Controls.Add(ogr_Uc);

            öğrencilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            bölümlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            borçlarToolStripMenuItem.BackColor = Color.LightSkyBlue;
            giderlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            personellerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            odalarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            kasaToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            yöneticilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void giderlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            container_pnl.Controls.Clear();
            gider_uc ogr_Uc = new gider_uc();
            ogr_Uc.Dock = DockStyle.Fill;
            container_pnl.Controls.Add(ogr_Uc);

            öğrencilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            bölümlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            borçlarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            giderlerToolStripMenuItem.BackColor = Color.LightSkyBlue;
            personellerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            odalarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            kasaToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            yöneticilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void personellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            container_pnl.Controls.Clear();
            personel_uc ogr_Uc = new personel_uc();
            ogr_Uc.Dock = DockStyle.Fill;
            container_pnl.Controls.Add(ogr_Uc);

            öğrencilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            bölümlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            borçlarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            giderlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            personellerToolStripMenuItem.BackColor = Color.LightSkyBlue;
            odalarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            kasaToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            yöneticilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void odalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            container_pnl.Controls.Clear();
            odalar_uc ogr_Uc = new odalar_uc();
            ogr_Uc.Dock = DockStyle.Fill;
            container_pnl.Controls.Add(ogr_Uc);

            öğrencilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            bölümlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            borçlarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            giderlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            personellerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            odalarToolStripMenuItem.BackColor = Color.LightSkyBlue;
            kasaToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            yöneticilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void kasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            container_pnl.Controls.Clear();
            geliristatistik_uc ogr_Uc = new geliristatistik_uc();
            ogr_Uc.Dock = DockStyle.Fill;
            container_pnl.Controls.Add(ogr_Uc);

            öğrencilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            bölümlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            borçlarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            giderlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            personellerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            odalarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            kasaToolStripMenuItem.BackColor = Color.LightSkyBlue;
            yöneticilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void yöneticilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            container_pnl.Controls.Clear();
            yonetici_pnl_uc ogr_Uc = new yonetici_pnl_uc();
            ogr_Uc.Dock = DockStyle.Fill;
            container_pnl.Controls.Add(ogr_Uc);

            öğrencilerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            bölümlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            borçlarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            giderlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            personellerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            odalarToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            kasaToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.Control);
            yöneticilerToolStripMenuItem.BackColor = Color.LightSkyBlue;
        }
    }
}
