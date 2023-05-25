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

namespace kargo_takip
{
    public partial class anasayfa : Form
    {
        
        public anasayfa()
        {
            InitializeComponent();
        }

        public SqlConnection bagla = new SqlConnection("data source = RAMO\\SQLEXPRESS; initial catalog = kargo_takip; integrated security = SSPI");
        public DataSet ds = new DataSet();
        public static string s;
        private void button1_Click(object sender, EventArgs e)
        {   //uye login
            bagla.Open();
            SqlCommand kmt = new SqlCommand("select id,mail,sifre from uyeler where mail=@mail and sifre=@sifre" , bagla);
            kmt.Parameters.AddWithValue("@mail", textBox1.Text);
            kmt.Parameters.AddWithValue("@sifre",textBox2.Text);
            SqlDataReader oku = kmt.ExecuteReader();
            if (oku.Read())
            {
                s = oku[0].ToString();
                this.Hide();
                uye uye = new uye();
                uye.Show();
            }
            else if (textBox1.Text=="" && textBox2.Text=="")
            {
                MessageBox.Show("Lütfen mail ve şifrenizi giriniz");
            }
            else
            {
                MessageBox.Show("Mail veya şifreniz yanlış");
            }
            bagla.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {   //uye kayit
            this.Hide();
            uye_ol uye_ol = new uye_ol();
            uye_ol.Show();
        }

        private void kargo_anasayfa_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }


        private void button4_Click(object sender, EventArgs e)
        {   //personel login
            bagla.Open();
            SqlCommand kmt = new SqlCommand("select kullanici_ad,sifre from personel where kullanici_ad=@kad and sifre=@sifre", bagla);
            kmt.Parameters.AddWithValue("@kad", textBox4.Text);
            kmt.Parameters.AddWithValue("@sifre", textBox3.Text);
            SqlDataReader oku = kmt.ExecuteReader();
            if (oku.Read())
            {
                this.Hide();
                personel personel = new personel();
                personel.Show();
            }
            else if (textBox4.Text=="" && textBox3.Text=="")
            {
                MessageBox.Show("Lütfen boş bırakmayınız");
            }
            else
            {
                MessageBox.Show("Kullanıcı adınız veya şifreniz yanlış");
            }
            bagla.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {   //takip no'ya göre sorgulama
            bagla.Open();
            SqlCommand kmt = new SqlCommand("select * from gonderi_takip where takip_no=@takip_no", bagla);
            kmt.Parameters.AddWithValue("@takip_no", textBox6.Text);
            SqlDataReader oku = kmt.ExecuteReader();

            if (oku.Read())
            {
                bagla.Close();
                bagla.Open();
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter("select takip_no as 'Takip No',durum as 'Durumu',gonderi_tarih as 'Gönderim Tarihi' from gonderi_takip where takip_no='" + textBox6.Text + "'", bagla);
                da.Fill(ds, "gonderi_takip");
                dataGridView1.DataSource = ds.Tables["gonderi_takip"];
                bagla.Close();
                textBox6.Clear();
            }
            else if (textBox6.Text == "")
            {
                MessageBox.Show("Takip numarınızı giriniz");
                bagla.Close();
            }
            else
            {
                MessageBox.Show("Geçersiz takip no");
                bagla.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {   //ad soyada göre sorgulama
            bagla.Open();
            SqlCommand kmt = new SqlCommand("select * from gonderi_takip where alici_ad=@ad and alici_soyad=@sad", bagla);
            kmt.Parameters.AddWithValue("@ad", textBox5.Text);
            kmt.Parameters.AddWithValue("@sad", textBox7.Text);
            SqlDataReader oku = kmt.ExecuteReader();
            if (oku.Read())
            {
                bagla.Close();
                bagla.Open();
                dataGridView2.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter("select takip_no as 'Takip No',durum as 'Durumu',gonderi_tarih as 'Gönderim Tarihi' from gonderi_takip where alici_ad='" + textBox5.Text + "' and alici_soyad='"+textBox7.Text+"'", bagla);
                da.Fill(ds, "gonderi_takip");
                dataGridView2.DataSource = ds.Tables["gonderi_takip"];
                ds.Clear();
                bagla.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı");
                bagla.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {   //tarihe göre sorgulama
            bagla.Open();
            SqlCommand kmt = new SqlCommand("select * from gonderi_takip where teslim_tarih between @t1 and @t2", bagla);
            kmt.Parameters.AddWithValue("@t1", dateTimePicker1.Value.Date.ToShortDateString());
            kmt.Parameters.AddWithValue("@t2", dateTimePicker2.Value.Date.ToShortDateString());
            SqlDataReader oku = kmt.ExecuteReader();
            if (oku.Read())
            {
                ds.Clear();
                bagla.Close();
                bagla.Open();
                dataGridView3.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter("select takip_no as 'Takip No',durum as 'Durumu',gonderi_tarih as 'Gönderim Tarihi' from gonderi_takip where teslim_tarih between '" + dateTimePicker1.Value.Date.ToShortDateString() + "' and '"+ dateTimePicker2.Value.Date.ToShortDateString() + "'", bagla) ;
                da.Fill(ds, "gonderi_takip");
                dataGridView3.DataSource = ds.Tables["gonderi_takip"];
                bagla.Close();
            }
            else
            {
                bagla.Close();
            }
        }
    }
}
