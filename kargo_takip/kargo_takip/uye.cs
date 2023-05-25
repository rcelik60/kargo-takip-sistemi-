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
    public partial class uye : Form
    {
        public uye()
        {
            InitializeComponent();
        }

        public SqlConnection bagla = new SqlConnection("data source = RAMO\\SQLEXPRESS; initial catalog = kargo_takip; integrated security = SSPI");
        public DataSet ds = new DataSet();
        anasayfa anasayfa = new anasayfa();
        public void kargo_listele()
        {   //giris yapan uyenin kargolarını listelem
            string g_id = anasayfa.s.ToString();
            bagla.Open();
            SqlDataAdapter da = new SqlDataAdapter("select takip_no as 'Takip No',durum as 'Durumu',teslim_tipi as 'Teslimat Tipi',teslim_tarih as 'Teslimat Tarihi',gonderi_tarih as 'Gönderim Tarih',odeme_tipi as 'Ödeme Tipi',alici_adres as 'Alıcı Adres',alici_ad+' '+alici_soyad as 'Ad Soyad',alici_tel as 'Alıcı Telefon' from gonderi_takip where gonderen_id="+g_id, bagla);
            da.Fill(ds, "gonderi_takip");
            dataGridView1.DataSource = ds.Tables["gonderi_takip"];
            bagla.Close();
        }
        public void uye_bilgi()
        {   //uye bilgisini textboxa atma
            int g_id = Convert.ToInt32(anasayfa.s);
            bagla.Open();
            SqlCommand kmt = new SqlCommand("select * from uyeler where id=@g_id", bagla);
            kmt.Parameters.AddWithValue("@g_id", g_id);
            SqlDataReader oku = kmt.ExecuteReader();
            if (oku.Read())
            {
                textBox18.Text = oku[0].ToString();
                textBox17.Text = oku[1].ToString();
                textBox12.Text = oku[2].ToString();
                textBox13.Text = oku[3].ToString();
                textBox14.Text = oku[4].ToString();
                textBox11.Text = oku[5].ToString();
                textBox10.Text = oku[6].ToString();
            }
            bagla.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   //kargo insert
            Random rnd = new Random();
            int sayi = rnd.Next(100000, 999999);
            string durum = "Kargo Bayide";
            DateTime dt1 = DateTime.Today;
            DateTime dt2 = DateTime.Today.AddDays(3);
            string g_id = anasayfa.s.ToString();
            bagla.Open();
            SqlCommand kmt = new SqlCommand("insert into gonderi_takip(takip_no,durum,teslim_tipi,teslim_tarih,gonderi_tarih,odeme_tipi,alici_adres,alici_ad,alici_soyad,alici_tel,gonderen_id) values(@takip_no,@durum,@tt,@ttarih,@gtarih,@od,@adres,@ad,@sad,@tel,@g_id)", bagla);
            kmt.Parameters.AddWithValue("@takip_no", sayi);
            kmt.Parameters.AddWithValue("@gtarih", dt1.ToShortDateString());
            kmt.Parameters.AddWithValue("@ttarih", dt2.ToShortDateString());
            kmt.Parameters.AddWithValue("@durum", durum);
            kmt.Parameters.AddWithValue("@ad", textBox1.Text);
            kmt.Parameters.AddWithValue("@sad", textBox2.Text);
            kmt.Parameters.AddWithValue("@tel", textBox3.Text);
            kmt.Parameters.AddWithValue("@od", comboBox1.SelectedItem.ToString());
            kmt.Parameters.AddWithValue("@tt", comboBox2.SelectedItem.ToString());
            kmt.Parameters.AddWithValue("@adres", textBox4.Text);
            kmt.Parameters.AddWithValue("@g_id", g_id);
            kmt.ExecuteNonQuery();
            bagla.Close();
            ds.Clear();
            kargo_listele();
        }

        private void uye_Load(object sender, EventArgs e)
        {   
            kargo_listele();
            uye_bilgi();
            textBox17.Enabled = false;
            textBox12.Enabled = false;
            textBox13.Enabled = false;
            textBox14.Enabled = false;
            textBox11.Enabled = false;
            textBox10.Enabled = false;
            button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox17.Enabled = true;
            textBox12.Enabled = true;
            textBox13.Enabled = true;
            textBox14.Enabled = true;
            textBox11.Enabled = true;
            textBox10.Enabled = true;
            button3.Enabled = true;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {   //uye update
            bagla.Open();
            SqlCommand kmt = new SqlCommand("update uyeler set ad = @ad, soyad = @soyad, dogum_yili=@dy,tel = @tel,mail=@mail,sifre=@sifre where id = @id", bagla);
            kmt.Parameters.AddWithValue("@id", Convert.ToInt32(textBox18.Text));
            kmt.Parameters.AddWithValue("@ad", textBox17.Text);
            kmt.Parameters.AddWithValue("@soyad", textBox12.Text);
            kmt.Parameters.AddWithValue("@dy", textBox13.Text);
            kmt.Parameters.AddWithValue("@tel", textBox14.Text);
            kmt.Parameters.AddWithValue("@mail", textBox11.Text);
            kmt.Parameters.AddWithValue("@sifre", textBox10.Text);
            kmt.ExecuteNonQuery();
            bagla.Close();
            uye_bilgi();
            MessageBox.Show("Bilgileriniz güncellendi");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            anasayfa anasayfa = new anasayfa();
            anasayfa.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}