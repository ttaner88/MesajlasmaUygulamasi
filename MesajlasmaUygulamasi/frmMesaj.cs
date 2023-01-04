using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesajlasmaUygulamasi
{
    public partial class frmMesaj : Form
    {
        public frmMesaj()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=TTANNOS;Initial Catalog=MesajUygulamasi;Integrated Security=True");

        public string numara;


        void gelenkutusu()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select MESAJID,(ad+' '+soyad) as GONDEREN,BASLIK,ICERIK From MESAJ inner join KISILER on MESAJ.GONDEREN = KISILER.NUMARA where alıcı=" + numara, baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dgGelenKutusu.DataSource = dt1;
        }
        void gidenkutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select MESAJID,(ad+' '+soyad) as ALICI ,BASLIK,ICERIK From MESAJ inner join KISILER on MESAJ.ALICI = KISILER.NUMARA where GONDEREN=" + numara, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dgGidenKutusu.DataSource = dt2;
        }

        private void frmMesaj_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;
            gelenkutusu();
            gidenkutusu();

            //Ad Soyad Çekme

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select ad,soyad from kısıler where  numara=" + numara, baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            baglanti.Close();
        }

        

        private void btnGonder_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand gonder = new SqlCommand("insert into MESAJ(GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)", baglanti);
            gonder.Parameters.AddWithValue("@p1", numara);
            gonder.Parameters.AddWithValue("@p2", mskAlici.Text);
            gonder.Parameters.AddWithValue("@p3", txtBaslik.Text);
            gonder.Parameters.AddWithValue("@p4", rchMesaj.Text);
            gonder.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesajınız İletildi");
            gelenkutusu();
            gidenkutusu();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
