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

namespace MesajlasmaUygulamasi
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=TTANNOS;Initial Catalog=MesajUygulamasi;Integrated Security=True");

        int hak = 3;

        private void btnGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand giris = new SqlCommand("Select * from KISILER where NUMARA = @P1 and SIFRE=@P2 ", baglanti);
            giris.Parameters.AddWithValue("@p1", txtNumara.Text);
            giris.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = giris.ExecuteReader();
            if (dr.Read())
            {
                frmMesaj fr = new frmMesaj();
                fr.numara = txtNumara.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                hak--;
                if (hak == 0)
                {
                    MessageBox.Show("Giriş Durduruldu","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    btnGiris.Enabled = false;
                }
                MessageBox.Show("Hatalı Giriş. Tekrar Deneyiniz.", " Kalan Deneme Hakkınız: " + hak,MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            baglanti.Close();
            
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
}
