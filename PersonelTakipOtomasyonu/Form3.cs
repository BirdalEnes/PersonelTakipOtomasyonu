using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PersonelTakipOtomasyonu
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = personeltakip; Integrated Security = True");

        private void personelleri_goster()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select tcno AS[Tc Kimlik No], ad AS[ADI], soyad AS[SOYADI],cinsiyet AS[CİNSİYET],mezuniyet AS[MEZUNİYETİ],dogumtarihi AS[DOĞUM TARİHİ], gorevi AS[GÖREVİ], gorevyeri AS[GÖREV YERİ], maasi AS[MAAŞI] from personeller Order By ad ASC", baglanti);
                DataSet ds = new DataSet();
                dataGridView1.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            personelleri_goster();
            this.Text = "Kullanıcı İşlemleri";
            lblAktifKullanıcı.Text = Form1.adi + "" + Form1.soyadi;

            pictureBox1.Height = 150; pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;

            pictureBox2.Height = 150; pictureBox2.Width = 150;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;

            try
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");
            }
            catch
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");
            }
            maskedTextBox1.Mask = "00000000000";
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            bool kayıt_arama_durumu = false;
            if (maskedTextBox1.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    kayıt_arama_durumu = true;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + read.GetValue(0) + ".jpg");
                    }
                    catch
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.jpg");
                    }
                    lablAd.Text = read.GetValue(1).ToString();
                    lblSoyad.Text = read.GetValue(2).ToString();
                    if (read.GetValue(3).ToString() == "Bay")
                        lblCinsiyet.Text = "Bay";
                    else
                        lblCinsiyet.Text = "Bayan";
                    lblMezuniyet.Text = read.GetValue(4).ToString();
                    lblDogumTarihi.Text = read.GetValue(5).ToString();
                    lblGorevi.Text = read.GetValue(6).ToString();
                    lblGorevYeri.Text = read.GetValue(7).ToString();
                    lblMaas.Text = read.GetValue(8).ToString();
                    break;
                }
                if (kayıt_arama_durumu == false)
                    MessageBox.Show("Aranan Kayıt Bulunmadı");
                baglanti.Close();
            }
            else
                MessageBox.Show("Lütfen 11 Haneli Bir TC kimlik Numarası Giriniz");

        }
    }
}
