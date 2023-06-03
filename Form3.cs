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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VeterinerHekimKayıtSistemi
{
    public partial class Form3 : Form
    {
        public Form3()
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
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("Select * from TedaviBilgileri");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into TedaviBilgileri(TedaviEdilenHayvanİD,TedaviAciklama,TedaviTarihi) values (@hayvanid,@tedacik,@tedtarih)", baglantı);

            komut.Parameters.AddWithValue("@hayvanid", textBox2.Text);
            komut.Parameters.AddWithValue("@tedacik", textBox3.Text);
            komut.Parameters.AddWithValue("@tedtarih", dateTimePicker1.Value);
            komut.ExecuteNonQuery();
            verilerigoster("Select * from TedaviBilgileri");
            baglantı.Close();

            textBox2.Clear();
            textBox3.Clear();
            
            textBox2.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("delete from TedaviBilgileri where TedaviİD=@tedid", baglantı);
            komut.Parameters.AddWithValue("@tedid", Convert.ToInt32(textBox1.Text));
            komut.ExecuteNonQuery();
            verilerigoster("select * from TedaviBilgileri");
            baglantı.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("UPDATE TedaviBilgileri SET TedaviEdilenHayvanİD=@hayvanid, TedaviAciklama=@tedacik,TedaviTarihi=@tedtarih WHERE TedaviİD=@tedid", baglantı);
            komut.Parameters.AddWithValue("@tedid", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@hayvanid", textBox2.Text);
            komut.Parameters.AddWithValue("@tedacik", textBox3.Text);
          
            komut.Parameters.AddWithValue("@tedtarih", dateTimePicker1.Value);
            komut.ExecuteNonQuery();
            verilerigoster("select * from TedaviBilgileri");
            baglantı.Close();

        }
    }
}
