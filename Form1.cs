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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-R3KOK53\\SQLEXPRESS;Initial Catalog=VeterinerHekimKayıtSistemiDb;Integrated Security=True");

        public void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglantı);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("Select * from HayvanBilgileri");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into HayvanBilgileri(HayvanAdi,HayvanTuru,Cinsiyet,DogumTarihi) values (@hayvanad,@hayvantur,@hayvancins,@dogumtarih)", baglantı);
            
            komut.Parameters.AddWithValue("@hayvanad", textBox2.Text);
            komut.Parameters.AddWithValue("@hayvantur", textBox3.Text);
            komut.Parameters.AddWithValue("@hayvancins", textBox4.Text);
            komut.Parameters.AddWithValue("@dogumtarih", dateTimePicker1.Value);
            komut.ExecuteNonQuery();
            verilerigoster("Select * from HayvanBilgileri");
            baglantı.Close();

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox2.Focus();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("delete from HayvanBilgileri where HayvanİD=@hayvanid", baglantı);
            komut.Parameters.AddWithValue("@hayvanid",Convert.ToInt32( textBox1.Text));
            komut.ExecuteNonQuery();
            verilerigoster("select * from HayvanBilgileri");
            baglantı.Close();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("UPDATE HayvanBilgileri SET HayvanAdi=@hayvanad, HayvanTuru=@hayvantur,Cinsiyet=@hayvancins,DogumTarihi=@dogumtarih WHERE HayvanİD=@hayvanid",baglantı);
            komut.Parameters.AddWithValue("@hayvanid", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@hayvanad",textBox2.Text);
            komut.Parameters.AddWithValue("@hayvantur", textBox3.Text);
            komut.Parameters.AddWithValue("@hayvancins", textBox4.Text);
            komut.Parameters.AddWithValue("@dogumtarih", dateTimePicker1.Value);
            komut.ExecuteNonQuery();
            verilerigoster("select * from HayvanBilgileri");
            baglantı.Close();








        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            
            form2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Form3 form3 = new Form3();

            form3.Show();
        }
    }
}
