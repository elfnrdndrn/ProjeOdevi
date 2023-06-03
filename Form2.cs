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
namespace VeterinerHekimKayıtSistemi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-R3KOK53\\SQLEXPRESS;Initial Catalog=VeterinerHekimKayıtSistemiDb;Integrated Security=True");

        public void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglantı);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }





        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
           textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("Select * from SahipBilgileri");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into SahipBilgileri(AdSoyad,TelefonNumarasi,eposta_adresi,SahipOlduguHayvanİD) values (@adsyd,@tlfno,@posta,@hayvanid)", baglantı);

            komut.Parameters.AddWithValue("@adsyd", textBox2.Text);
            komut.Parameters.AddWithValue("@tlfno", textBox3.Text);
            komut.Parameters.AddWithValue("@posta", textBox4.Text);
            komut.Parameters.AddWithValue("@hayvanid",textBox5.Text);
            komut.ExecuteNonQuery();
            verilerigoster("Select * from SahipBilgileri");
            baglantı.Close();

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox2.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("delete from SahipBilgileri where SahipİD=@sahipid", baglantı);
            komut.Parameters.AddWithValue("@sahipid", Convert.ToInt32(textBox1.Text));
            komut.ExecuteNonQuery();
            verilerigoster("select * from SahipBilgileri");
            baglantı.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("UPDATE SahipBilgileri SET AdSoyad=@adsyd, TelefonNumarasi=@tlfno,eposta_adresi=@posta,SahipOlduguHayvanİD=@hayvanid WHERE SahipİD=@sahipid", baglantı);
            komut.Parameters.AddWithValue("@sahipid", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@adsyd", textBox2.Text);
            komut.Parameters.AddWithValue("@tlfno", textBox3.Text);
            komut.Parameters.AddWithValue("@posta", textBox4.Text);
            komut.Parameters.AddWithValue("@hayvanid", textBox5.Text);
            komut.ExecuteNonQuery();
            verilerigoster("select * from SahipBilgileri");
            baglantı.Close();
        }
    }
}
