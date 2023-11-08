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

namespace dtgv2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-BJO2DGU\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True");

        public void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler,baglan);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("select * from tblKitaplar");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("insert into tblKitaplar (ad,yazar,sayfaNo,basımEvi,tür) values (@p1,@p2,@p3,@p4,@p5)",baglan);
            komut.Parameters.AddWithValue("@p1",txAd.Text);
            komut.Parameters.AddWithValue("@p2",txYazar.Text);
            komut.Parameters.AddWithValue("@p3",txSayfa.Text);
            komut.Parameters.AddWithValue("@p4",txBasimEvi.Text);
            komut.Parameters.AddWithValue("@p5",txTuru.Text);
            komut.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Kitap Eklendi");
            txAd.Clear();
            txYazar.Clear();
            txSayfa.Clear();
            txBasimEvi.Clear();
            txTuru.Clear();
            verilerigoster("select * from tblKitaplar");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("delete from tblKitaplar where ad=@p1",baglan);
            komut.Parameters.AddWithValue("@p1",textBox1.Text);
            komut.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Kitap Silindi");
            textBox1.Clear();
            textBox2.Clear();
            txAd.Clear();
            txYazar.Clear();
            txSayfa.Clear();
            txBasimEvi.Clear();
            txTuru.Clear();
            verilerigoster("select * from tblKitaplar");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from tblKitaplar where ad like '%"+textBox2.Text+"%'",baglan);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            baglan.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //ad,yazar,sayfaNo,basımEvi,tür
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            string ad = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            string yazar = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
            string sayfano = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
            string basimevi = dataGridView1.Rows[secilialan].Cells[3].Value.ToString();
            string tur = dataGridView1.Rows[secilialan].Cells[4].Value.ToString();

            txAd.Text = ad;
            txYazar.Text = yazar;
            txSayfa.Text = sayfano;
            txBasimEvi.Text = basimevi;
            txTuru.Text = tur;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("update tblKitaplar set yazar=@a2,sayfaNo=@a3,basımEvi=@a4, tür=@a5 where ad=@a1",baglan);
            komut.Parameters.AddWithValue("@a1",txAd.Text);
            komut.Parameters.AddWithValue("@a2",txYazar.Text);
            komut.Parameters.AddWithValue("@a3",txSayfa.Text);
            komut.Parameters.AddWithValue("@a4",txBasimEvi.Text);
            komut.Parameters.AddWithValue("@a5",txTuru.Text);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerigoster("select * from tblKitaplar");
            textBox1.Clear();
            textBox2.Clear();
            txAd.Clear();
            txYazar.Clear();
            txSayfa.Clear();
            txBasimEvi.Clear();
            txTuru.Clear();
            MessageBox.Show("Kitap Güncellendi");
        }
    }
}
