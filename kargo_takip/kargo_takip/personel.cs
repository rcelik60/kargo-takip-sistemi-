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
    public partial class personel : Form
    {
        public personel()
        {
            InitializeComponent();
        }

        public SqlConnection bagla = new SqlConnection("data source = RAMO\\SQLEXPRESS; initial catalog = kargo_takip; integrated security = SSPI");
        public DataSet ds = new DataSet();

        public void listele()
        {   //kargo listeleme
            ds.Clear();
            bagla.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from gonderi_takip", bagla);
            da.Fill(ds, "gonderi_takip");
            dataGridView1.DataSource = ds.Tables["gonderi_takip"];
            bagla.Close();
        }
        public void listele1()
        {   //uye listeleme
            ds.Clear();
            bagla.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from uyeler", bagla);
            da.Fill(ds, "uyeler");
            dataGridView2.DataSource = ds.Tables["uyeler"];
            bagla.Close();
        }

        private void personel_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listele();
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            groupBox2.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {   //kargo update
            bagla.Open();
            SqlCommand kmt = new SqlCommand("update gonderi_takip set takip_no = @t_no, durum = @durum, teslim_tipi=@t_tip,teslim_tarih = @t_tarih,gonderi_tarih=@g_tarih,odeme_tipi=@o_tip,alici_adres=@adres,alici_ad=@ad,alici_soyad=@sad,alici_tel=@tel,gonderen_id=@g_id where id = @id", bagla);
            kmt.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
            kmt.Parameters.AddWithValue("@t_no", textBox2.Text);
            kmt.Parameters.AddWithValue("@durum", comboBox1.Text);
            kmt.Parameters.AddWithValue("@t_tip", comboBox2.Text);
            kmt.Parameters.AddWithValue("@t_tarih", textBox3.Text);
            kmt.Parameters.AddWithValue("@g_tarih", textBox4.Text);
            kmt.Parameters.AddWithValue("@o_tip", comboBox3.Text);
            kmt.Parameters.AddWithValue("@ad", textBox6.Text);
            kmt.Parameters.AddWithValue("@sad", textBox7.Text);
            kmt.Parameters.AddWithValue("@tel", textBox8.Text);
            kmt.Parameters.AddWithValue("@g_id", textBox9.Text);
            kmt.Parameters.AddWithValue("@adres", textBox5.Text);
            kmt.ExecuteNonQuery();
            ds.Clear();
            bagla.Close();
            listele();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            dataGridView2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listele1();
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            groupBox1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            dataGridView1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {   //uye update
            bagla.Open();
            SqlCommand kmt = new SqlCommand("update uyeler set ad = @ad, soyad = @soyad, dogum_yili=@dy,tel = @tel,mail=@mail,sifre=@sifre where id = @id", bagla);
            kmt.Parameters.AddWithValue("@id", dataGridView2.CurrentRow.Cells[0].Value);
            kmt.Parameters.AddWithValue("@ad", textBox17.Text);
            kmt.Parameters.AddWithValue("@soyad", textBox12.Text);
            kmt.Parameters.AddWithValue("@dy", textBox13.Text);
            kmt.Parameters.AddWithValue("@tel", textBox14.Text);
            kmt.Parameters.AddWithValue("@mail", textBox11.Text);
            kmt.Parameters.AddWithValue("@sifre", textBox10.Text);
            kmt.ExecuteNonQuery();
            ds.Clear();
            bagla.Close();
            listele1();
            textBox18.Clear();
            textBox17.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox11.Clear();
            textBox10.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {   //kargo bilgi texte atma
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {   //musteri bilgi texte atma
            textBox18.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox17.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox12.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox13.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            textBox14.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            textBox11.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            textBox10.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {   //musteri delete
            bagla.Open();
            SqlCommand kmt = new SqlCommand("delete uyeler where id = @id", bagla);
            kmt.Parameters.AddWithValue("@id", dataGridView2.CurrentRow.Cells[0].Value);
            kmt.ExecuteNonQuery();
            ds.Clear();
            bagla.Close();
            listele1();
            textBox18.Clear();
            textBox17.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox11.Clear();
            textBox10.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {   //kargo delete
            bagla.Open();
            SqlCommand kmt = new SqlCommand("delete gonderi_takip where id = @id", bagla);
            kmt.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
            kmt.ExecuteNonQuery();
            ds.Clear();
            bagla.Close();
            listele();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            anasayfa anasayfa = new anasayfa();
            anasayfa.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
