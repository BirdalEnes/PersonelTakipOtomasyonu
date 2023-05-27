using System;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace PersonelTakipOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=personeltakip;Integrated Security=True");

        //formlar arası veri aktarımında kullanılacak değişkenler
        public static string tcno, adi, soyadi, yetki;
        // bu formada kullanılacak değişkenler
        bool durum = false;
        int hak = 3;

        private void btnradioKullanıcı_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btngiris;
            this.CancelButton = btncıkıs;
            label5.Text = Convert.ToString(hak);
            btnradioYonetıcı.Checked = true;

        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from Kullanicilar", baglanti);
                SqlDataReader read = komut.ExecuteReader();

                while (read.Read())
                {
                    if (btnradioYonetıcı.Checked == true)
                    {
                        if (read["kullaniciadi"].ToString() == txtkullaniciadi.Text && read["parola"].ToString() == txtsifre.Text && read["yetki"].ToString() == "yönetici")
                        {
                            durum = true;
                            tcno = read.GetValue(0).ToString();
                            adi = read.GetValue(1).ToString();
                            soyadi = read.GetValue(2).ToString();
                            yetki = read.GetValue(3).ToString();
                            Form2 frm2 = new Form2();
                            frm2.Show();

                            //this.Hide(); diğer sayfayı kapatıp öbür sayfa tek açık kalıyor
                            break;
                        }
                    }

                    if (btnradioKullanıcı.Checked == true)
                    {
                        if (read["kullaniciadi"].ToString() == txtkullaniciadi.Text && read["parola"].ToString() == txtsifre.Text && read["yetki"].ToString() == "kullanıcı")
                        {
                            durum = true;
                            tcno = read.GetValue(0).ToString();
                            adi = read.GetValue(1).ToString();
                            soyadi = read.GetValue(2).ToString();
                            yetki = read.GetValue(3).ToString();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            //this.Hide(); diğer sayfayı kapatıp öbür sayfa tek açık kalıyor
                            break;
                        }
                    }
                }
                if (durum == false)
                    hak--;
                baglanti.Close();
            }
            label5.Text = Convert.ToString(hak);
            if (hak == 0)
            {
                btngiris.Enabled = false;
                MessageBox.Show("Giriş Hakkı Kalmadı !", "personel takip otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }




    }
}
