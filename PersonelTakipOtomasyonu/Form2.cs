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
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlTypes;

namespace PersonelTakipOtomasyonu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=personeltakip;Integrated Security=True");

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'personeltakipDataSet2.personeller' table. You can move, or remove it, as needed.
            this.personellerTableAdapter.Fill(this.personeltakipDataSet2.personeller);
            // TODO: This line of code loads data into the 'personeltakipDataSet1.Kullanicilar' table. You can move, or remove it, as needed.
            this.kullanicilarTableAdapter1.Fill(this.personeltakipDataSet1.Kullanicilar);
            // TODO: This line of code loads data into the 'personeltakipDataSet.Kullanicilar' table. You can move, or remove it, as needed.
            this.kullanicilarTableAdapter.Fill(this.personeltakipDataSet.Kullanicilar);

            /*Form2 Ayarları
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            pictureBox1.SizeMode=PictureBoxSizeMode.StretchImage;
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");
            }
            catch (Exception)
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg" );

            }
            */

            //Kullanıcı İşlemleri Sekmesi
            this.Text = "Yönetici İşlemleri";
            label11.ForeColor = Color.DarkBlue;
            label11.Text = Form1.adi + " " + Form1.soyadi;
            txtTc.MaxLength = 11;
            txtKullaniciadi.MaxLength = 8;
            toolTip1.SetToolTip(this.txtTc, "Tc Kimlik No 11 Karekterli Olmalıdır");
            radioYonetici.Checked = true;
            txtAd.CharacterCasing = CharacterCasing.Upper;
            txtSoyad.CharacterCasing = CharacterCasing.Upper;
            txtparola.MaxLength = 10;
            txtParolaTekrar.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            kullanicilari_goster();

            //Personel İşlemleri Sekmesi
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100; pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            maskedTextTC.Mask = "00000000000";
            maskedTextAd.Mask = "LL????????????????????";
            maskedTextSoyad.Mask = "LL????????????????????";
            maskedTextMaaş.Mask = "0000";
            maskedTextMaaş.Mask = "0";
            maskedTextAd.Text.ToUpper();
            maskedTextSoyad.Text.ToUpper();

            cmbMezuniyet.Items.Add("İlkÖğretim");
            cmbMezuniyet.Items.Add("OrtaÖğretim");
            cmbMezuniyet.Items.Add("Lise");
            cmbMezuniyet.Items.Add("Üniversite");

            cmbGorevi.Items.Add("Yönetici");
            cmbGorevi.Items.Add("Memur");
            cmbGorevi.Items.Add("Şoför");
            cmbGorevi.Items.Add("İşci");

            cmbGorevYeri.Items.Add("Arge");
            cmbGorevYeri.Items.Add("Bilgi İşlem");
            cmbGorevYeri.Items.Add("Muhasebe");
            cmbGorevYeri.Items.Add("Üretim");
            cmbGorevYeri.Items.Add("Paketleme");
            cmbGorevYeri.Items.Add("Nakliye");

            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));
            dateTimePicker1.MinDate = new DateTime(yil, ay, gun);
            dateTimePicker1.MaxDate = new DateTime(yil, ay, gun);
            dateTimePicker1.Format = DateTimePickerFormat.Short;

            radioButtonBay.Checked = true;
            personelleri_goster();


        }
        private void kullanicilari_goster()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select tcno AS[Tc Kimlik No], ad AS[Adı], soyad AS[Soyadı], yetki AS[Yetki], kullaniciadi AS[KullanıcıAdı],parola AS[Parola] from Kullanicilar Order By ad ASC", baglanti);
                DataSet daset = new DataSet();
                // komut.Fill(daset);
                dataGridView1.DataSource = daset.Tables[0];
                baglanti.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }

        }
        private void personelleri_goster()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select tcno AS[Tc Kimlik No], ad AS[Adı], soyad AS[Soyadı], cinsiyet AS[Cinsiyeti], mezuniyet AS[Mezuniyeti], dogumtarihi AS[Doğum Tarihi], gorevi AS[Görevi], gorevyeri AS[Görev Yeri], maasi AS[Maaşı] from perrsoneller Order By ad ASC", baglanti);
                DataSet daset = new DataSet();
                // komut.Fill(daset);
                dataGridView2.DataSource = daset.Tables[0];
                baglanti.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }

        }


        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            if (txtTc.Text.Length < 11)
            {
                errorProvider1.SetError(txtTc, "Tc Kimlik No 11 Karekterli Olmalıdır");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtKullaniciadi_TextChanged(object sender, EventArgs e)
        {
            if (txtKullaniciadi.Text.Length != 8)
            {
                errorProvider1.SetError(txtKullaniciadi, "Kullanıcı adı 8 karekter olmalıdır");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtKullaniciadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        int parola_skoru = 0;
        private void txtparola_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = txtparola.Text;
            //türkçe  karekterleri ingilizce karekterlere dönüştürme
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');
            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                txtparola.Text = sifre;
                MessageBox.Show("Paroladaki türkçe karekterle ingilizce karekterlere dönüştürülmüştür");
            }
            //1 küçük harf 10 puan,2 ve üzeri 20 puan
            int az_karekter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karekter_sayisi) * 10;

            //1 büyük harf 10 puan,2 ve üzeri 20 puan
            int AZ_karekter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karekter_sayisi) * 10;

            //1 rakam 10 puan,2 ve üzeri 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            //1 sembol 10 puan, 2 ve üzeri 20 puan
            int sembol_sayisi = sifre.Length - az_karekter_sayisi - AZ_karekter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;

            if (sifre.Length == 9)
            {
                parola_skoru += 10;
            }
            else if (sifre.Length == 10)
            {
                parola_skoru += 20;
            }

            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
            {
                label22.Text = "Büyük harf , Küçük harf , rakam ve sembol mutlaka kullanmalısın";
            }
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
            {
                label22.Text = "";
            }
            if (parola_skoru < 70)
            {
                parola_seviyesi = "Kabul edilmez";
            }
            else if (parola_skoru == 70 || parola_skoru == 80)
            {
                parola_seviyesi = "Güçlü";
            }
            else if (parola_skoru == 90 || parola_skoru == 100)
            {
                parola_seviyesi = "Çok Güçlü";
            }
            label8.Text = "%" + Convert.ToString(parola_skoru);
            label9.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;
        }
        private void txtparola_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtParolaTekrar_TextChanged(object sender, EventArgs e)
        {
            if (txtParolaTekrar.Text != txtparola.Text)
            {
                errorProvider1.SetError(txtParolaTekrar, "Parolaa tekrarı eşleşmiyor");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void topPage1_Temizle()
        {
            txtTc.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtKullaniciadi.Clear();
            txtparola.Clear();
            txtParolaTekrar.Clear();
        }
        private void topPage2_Temizle()
        {
            pictureBox2.Image = null;
            maskedTextTC.Clear();
            maskedTextAd.Clear();
            maskedTextSoyad.Clear();
            maskedTextMaaş.Clear();
            cmbMezuniyet.SelectedIndex = -1;
            cmbGorevi.SelectedIndex = -1;
            cmbGorevYeri.SelectedIndex = -1;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol = false;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kullanicilar where tcno='" + txtTc.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglanti.Close();
            if (kayitkontrol == false)
            {
                // tc kontrol
                if (txtTc.Text.Length < 11 || txtTc.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                //ad kontrol
                if (txtAd.Text.Length < 2 || txtAd.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                //Soyad kontrol
                if (txtSoyad.Text.Length < 2 || txtSoyad.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                //Kullanıcı adı kontrol
                if (txtKullaniciadi.Text.Length != 8 || txtKullaniciadi.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                //Parola Kontrol
                if (parola_skoru < 70 || txtparola.Text == "")
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                //Parola tekrar Kontrol
                if (txtParolaTekrar.Text == "" || txtparola.Text != txtParolaTekrar.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;

                if (txtTc.Text.Length == 11 && txtTc.Text != "" && txtAd.Text != "" && txtAd.Text.Length > 1 && txtSoyad.Text != "" && txtSoyad.Text.Length > 1 && txtKullaniciadi.Text != "" && txtparola.Text != "" && txtParolaTekrar.Text != "" && txtparola.Text == txtParolaTekrar.Text && parola_skoru >= 70)
                {
                    if (radioYonetici.Checked == true)
                        yetki = "Yönetici";
                    else if (radioKullanici.Checked == true)
                        yetki = "Kullanıcı";
                    try
                    {
                        baglanti.Open();
                        SqlCommand komut2 = new SqlCommand("insert into kullanicilar values('" + txtTc.Text + "','" + txtAd.Text + "','" + txtSoyad.Text + "','" + yetki + "','" + txtKullaniciadi.Text + "','" + txtparola.Text + "','" + txtParolaTekrar.Text + "')", baglanti);
                        komut2.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Yeni Kullanıcı Kaydı oluşturuldu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_Temizle();
                    }
                    catch (Exception hatamsj)
                    {

                        MessageBox.Show(hatamsj.Message);
                        baglanti.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı olan alanları tekrardan giriniz", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC kimlik numarası ile daha önceden kayıt yapıldı", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if (txtTc.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from kullanicilar where tcno='" + txtTc.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    kayit_arama_durumu = true;
                    txtAd.Text = read.GetValue(1).ToString();
                    txtSoyad.Text = read.GetValue(2).ToString();
                    if (read.GetValue(3).ToString() == "Yönetici")
                        radioYonetici.Checked = true;
                    else
                        radioKullanici.Checked = true;
                    txtKullaniciadi.Text = read.GetValue(4).ToString();
                    txtparola.Text = read.GetValue(5).ToString();
                    txtParolaTekrar.Text = read.GetValue(5).ToString();
                    break;
                }
                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Aranan Kayıt Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 haneli TC kimlik numarasını giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_Temizle();
            }

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            string yetki = "";
            // tc kontrol
            if (txtTc.Text.Length < 11 || txtTc.Text == "")
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.Black;
            //ad kontrol
            if (txtAd.Text.Length < 2 || txtAd.Text == "")
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.Black;
            //Soyad kontrol
            if (txtSoyad.Text.Length < 2 || txtSoyad.Text == "")
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.Black;
            //Kullanıcı adı kontrol
            if (txtKullaniciadi.Text.Length != 8 || txtKullaniciadi.Text == "")
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.Black;
            //Parola Kontrol
            if (parola_skoru < 70 || txtparola.Text == "")
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.Black;
            //Parola tekrar Kontrol
            if (txtParolaTekrar.Text == "" || txtparola.Text != txtParolaTekrar.Text)
                label7.ForeColor = Color.Red;
            else
                label7.ForeColor = Color.Black;

            if (txtTc.Text.Length == 11 && txtTc.Text != "" && txtAd.Text != "" && txtAd.Text.Length > 1 && txtSoyad.Text != "" && txtSoyad.Text.Length > 1 && txtKullaniciadi.Text != "" && txtparola.Text != "" && txtParolaTekrar.Text != "" && txtparola.Text == txtParolaTekrar.Text && parola_skoru >= 70)
            {
                if (radioYonetici.Checked == true)
                    yetki = "Yönetici";
                else if (radioKullanici.Checked == true)
                    yetki = "Kullanıcı";
                try
                {
                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("update  kullanicilar set ad='" + txtAd.Text + "',soyad='" + txtSoyad.Text + "',yetki= '" + yetki + "',kullanici='" + txtKullaniciadi.Text + "',parola='" + txtparola.Text + "') where tcno='" + txtTc.Text + "'", baglanti);
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kullanıcı Bilgileri Güncellendi ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    kullanicilari_goster();
                }
                catch (Exception hatamsj)
                {

                    MessageBox.Show(hatamsj.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }

            }
            else
            {
                MessageBox.Show("Yazı Rengi Kırmızı olan alanları tekrardan giriniz", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtTc.Text.Length == 11)
            {
                bool kayıt_arama_durumu = false;
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from kullanicilar where tcno='" + txtTc.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    kayıt_arama_durumu = true;
                    SqlCommand komut2 = new SqlCommand("delete from kullanicilar where tcno='" + txtTc.Text + "'", baglanti);
                    komut2.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı Kaydı Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    kullanicilari_goster();
                    topPage1_Temizle();
                    break;
                }
                if (kayıt_arama_durumu == false)
                    MessageBox.Show("Silinecek Kayıt Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
                topPage1_Temizle();


            }
            else
            {
                MessageBox.Show("Lütfen 11 Karekterden Oluşan Bir TC Kimllik Numarası Girinz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            topPage1_Temizle();
        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            bool kayıtkontrol = false;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from personeller where tcno='" + maskedTextTC.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                kayıtkontrol = true;
                break;
            }
            baglanti.Close();
            if (kayıtkontrol == false)
            {
                if (pictureBox2.Image == null)
                    btnGozat.ForeColor = Color.Red;
                else
                    btnGozat.ForeColor = Color.Black;

                if (maskedTextTC.MaskCompleted == false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;

                if (maskedTextAd.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;

                if (maskedTextSoyad.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;

                if (cmbMezuniyet.Text == "")
                    label17.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;

                if (cmbGorevi.Text == "")
                    label19.ForeColor = Color.Red;
                else
                    label19.ForeColor = Color.Black;

                if (cmbGorevYeri.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;

                if (int.Parse(maskedTextMaaş.Text) < 1000)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (pictureBox2.Image != null && maskedTextTC.MaskCompleted != false && maskedTextAd.MaskCompleted != false && maskedTextSoyad.MaskCompleted != false && cmbMezuniyet.Text != "" && cmbGorevi.Text != "" && cmbGorevYeri.Text != "" && maskedTextMaaş.MaskCompleted != false)
                {
                    if (radioButtonBay.Checked == true)
                        cinsiyet = "Bay";
                    else if (radioButtonBayan.Checked == true)
                        cinsiyet = "Bayan";
                    try
                    {
                        baglanti.Open();
                        SqlCommand eklekomutu = new SqlCommand("insert into personeller values('" + maskedTextTC.Text + "','" + maskedTextAd.Text + "','" + maskedTextSoyad.Text + "','" + cinsiyet + "','" + cmbMezuniyet.Text + "','" + dateTimePicker1.Text + "','" + cmbGorevi.Text + "','" + cmbGorevYeri.Text + "','" + maskedTextMaaş.Text + "')", baglanti);
                        eklekomutu.ExecuteNonQuery();
                        baglanti.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\personelresimler"))
                            Directory.CreateDirectory(Application.StartupPath + "\\personelresimler");
                        else
                            pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\" + maskedTextTC.Text + ".jpg");
                        MessageBox.Show("Yeni personel kaydı oluşturuldu", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        personelleri_goster();
                        topPage2_Temizle();
                        maskedTextMaaş.Text = "0";
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alaranları Tekrar Gözden Geçirriniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("Girilen TC kimlik Numarası Daha Önceden Kayıtlıdır ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAramaYap_Click(object sender, EventArgs e)
        {
            bool kayıt_arama_durumu = false;
            if (maskedTextTC.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from personeller where tcno='" + maskedTextTC.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    kayıt_arama_durumu = true;
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + read.GetValue(0).ToString() + ".jpg");
                    }
                    catch
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.jpg");

                    }
                    maskedTextAd.Text = read.GetValue(1).ToString();
                    maskedTextSoyad.Text = read.GetValue(2).ToString();
                    if (read.GetValue(3).ToString() == "Bay")
                        radioButtonBay.Checked = true;
                    else
                        radioButtonBayan.Checked = true;
                    cmbMezuniyet.Text = read.GetValue(4).ToString();
                    dateTimePicker1.Text = read.GetValue(5).ToString();
                    cmbGorevi.Text = read.GetValue(6).ToString();
                    cmbGorevYeri.Text = read.GetValue(7).ToString();
                    maskedTextMaaş.Text = read.GetValue(8).ToString();
                    break;
                }
                if (kayıt_arama_durumu == false)
                    MessageBox.Show("Aranan Kayıt Bulunmadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("11 Haneli TC NO Giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            if (pictureBox2.Image == null)
                btnGozat.ForeColor = Color.Red;
            else
                btnGozat.ForeColor = Color.Black;

            if (maskedTextTC.MaskCompleted == false)
                label13.ForeColor = Color.Red;
            else
                label13.ForeColor = Color.Black;

            if (maskedTextAd.MaskCompleted == false)
                label14.ForeColor = Color.Red;
            else
                label14.ForeColor = Color.Black;

            if (maskedTextSoyad.MaskCompleted == false)
                label15.ForeColor = Color.Red;
            else
                label15.ForeColor = Color.Black;

            if (cmbMezuniyet.Text == "")
                label17.ForeColor = Color.Red;
            else
                label17.ForeColor = Color.Black;

            if (cmbGorevi.Text == "")
                label19.ForeColor = Color.Red;
            else
                label19.ForeColor = Color.Black;

            if (cmbGorevYeri.Text == "")
                label20.ForeColor = Color.Red;
            else
                label20.ForeColor = Color.Black;

            if (int.Parse(maskedTextMaaş.Text) < 1000)
                label21.ForeColor = Color.Red;
            else
                label21.ForeColor = Color.Black;

            if (pictureBox2.Image != null && maskedTextTC.MaskCompleted != false && maskedTextAd.MaskCompleted != false && maskedTextSoyad.MaskCompleted != false && cmbMezuniyet.Text != "" && cmbGorevi.Text != "" && cmbGorevYeri.Text != "" && maskedTextMaaş.MaskCompleted != false)
            {
                if (radioButtonBay.Checked == true)
                    cinsiyet = "Bay";
                else if (radioButtonBayan.Checked == true)
                    cinsiyet = "Bayan";
                try
                {
                    baglanti.Open();
                    SqlCommand guncellekomutu = new SqlCommand("update personeller set ad ='" + maskedTextAd.Text + "', soyad='" + maskedTextSoyad.Text + "',ciinsiyet='" + cinsiyet + "',mezuniyet='" + cmbMezuniyet.Text + "',dogumtarihi='" + dateTimePicker1.Text + "',gorevi='" + cmbGorevi.Text + "',gorevyeri='" + cmbGorevYeri.Text + "',mmaasi='" + maskedTextMaaş.Text + "' where tcno='" + maskedTextTC.Text + "'", baglanti);
                    guncellekomutu.ExecuteNonQuery();
                    baglanti.Close();

                    personelleri_goster();
                    topPage2_Temizle();
                    maskedTextMaaş.Text = "0";
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }

            }

        }

        private void buttonSıl_Click(object sender, EventArgs e)
        {
            if (maskedTextTC.MaskCompleted == true)
            {
                bool kayit_arama_durumu = false;
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from personeller where tcno='" + maskedTextTC.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    kayit_arama_durumu = true;
                    SqlCommand deletesorgu = new SqlCommand("delete from personeller where tcno='" + maskedTextTC.Text + "'", baglanti);
                    deletesorgu.ExecuteNonQuery();
                    break;
                }
                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglanti.Close();
                personelleri_goster();
                topPage2_Temizle();
                maskedTextMaaş.Text = "0";
            }
            else
            {
                MessageBox.Show("Lütfen 11 karekterden oluşan TC kimlik numarası Giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage2_Temizle();
                maskedTextMaaş.Text = "0";
            }
        }

        private void buttonTemızle_Click(object sender, EventArgs e)
        {
            topPage2_Temizle();
        }
    }
}



