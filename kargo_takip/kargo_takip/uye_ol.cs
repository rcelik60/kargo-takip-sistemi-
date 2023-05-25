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
    public partial class uye_ol : Form
    {
        public uye_ol()
        {
            InitializeComponent();
        }

        public SqlConnection bagla = new SqlConnection("data source = RAMO\\SQLEXPRESS; initial catalog = kargo_takip; integrated security = SSPI");
        public DataSet ds = new DataSet();
        private void button1_Click(object sender, EventArgs e)
        {
            bagla.Open();
            SqlCommand kmt = new SqlCommand("insert into uyeler(ad,soyad,dogum_yili,tel,mail,sifre) values(@ad,@sad,@dy,@tel,@mail,@sifre)", bagla);
            kmt.Parameters.AddWithValue("@ad", textBox1.Text);
            kmt.Parameters.AddWithValue("@sad", textBox2.Text);
            kmt.Parameters.AddWithValue("@dy", dateTimePicker1.Value.ToShortDateString());
            kmt.Parameters.AddWithValue("@tel", maskedTextBox1.Text);
            kmt.Parameters.AddWithValue("@mail", textBox6.Text);
            kmt.Parameters.AddWithValue("@sifre", textBox5.Text);
            kmt.ExecuteNonQuery();
            bagla.Close();
            if (textBox6.Text.Contains("@")==true && textBox6.Text.Contains(".com")==true)
            {
                MessageBox.Show("Kayıt Başarılı");
                this.Hide();
                anasayfa anasayfa = new anasayfa();
                anasayfa.Show();
            }
            else if (textBox6.Text=="" && textBox5.Text=="" && maskedTextBox1.Text=="" && textBox2.Text == "" && textBox1.Text == "")
            {
                MessageBox.Show("Lütfen bilgilerinizi giriniz");
            }
            else
            {
                MessageBox.Show("Hatalı mail");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            anasayfa anasayfa = new anasayfa();
            anasayfa.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
