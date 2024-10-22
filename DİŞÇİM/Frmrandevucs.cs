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

namespace DİŞÇİM
{
    public partial class Frmrandevucs : Form
    {
        public Frmrandevucs()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        ConnectionString MyCon = new ConnectionString();
        private void fillHasta()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select HAd from HastaTbl", baglanti);
            SqlDataReader RDR = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd", typeof(string));
            dt.Load(RDR);
            RadCb.ValueMember = "HAd";
            RadCb.DataSource = dt;
            baglanti.Close();


        }
        private void fillTedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select TAd from TedaviTbl", baglanti);
            SqlDataReader RDR = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TAd", typeof(string));
            dt.Load(RDR);
            Rtedavi.ValueMember = "TAd";
            Rtedavi.DataSource = dt;
            baglanti.Close();


        }
        private void Frmrandevucs_Load(object sender, EventArgs e)
        {
            fillHasta();
            fillTedavi();
            uyeler();
            Reset();
        }
        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select *from RandevuTbl  ";
            DataSet ds = Hs.Showhasta(query);
            RandevuDGV.DataSource = ds.Tables[0];

        }
        void Filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select *from RandevuTbl where Hasta like'%" + AraTb.Text + "%' ";
            DataSet ds = Hs.Showhasta(query);
            RandevuDGV.DataSource = ds.Tables[0];

        }
        void Reset()
        {
            RadCb.SelectedIndex = -1;
            Rtedavi.SelectedIndex = -1;
            Rtarih.Text = "";
            SaatCb.SelectedIndex = -1;
        }


        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string query = "insert into  RandevuTbl values('"+RadCb.SelectedValue.ToString() + "','" + Rtedavi.SelectedValue.ToString() + "','" + Rtarih.Text + "','"+SaatCb.Text+"')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("RANDEVU KAYDEDİLMİŞTİR");
                uyeler();
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);


            }
        }
        int key=0;
        private void button5_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek işlemi Seçiniz");

            }
            else
            {
                try
                {

                    string query = "Update RandevuTbl  set Hasta='" + RadCb.SelectedValue.ToString() + "',Tedavi='" + Rtedavi.SelectedValue.ToString() + "',RTarih='" + Rtarih.Text + "',RSaat='"+SaatCb.Text+"' where Rid=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Randevu işlemi Güncellendi");
                    uyeler();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void RandevuDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RadCb.SelectedValue = RandevuDGV.SelectedRows[0].Cells[1].Value.ToString();
            Rtedavi.SelectedValue = RandevuDGV.SelectedRows[0].Cells[2].Value.ToString();
            Rtarih.Text = RandevuDGV.SelectedRows[0].Cells[3].Value.ToString();
            SaatCb.Text= RandevuDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (RadCb.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(RandevuDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Randevuyu Seçiniz");

            }
            else
            {
                try
                {

                    string query = "delete from RandevuTbl where Rid=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Randevu işlemi Silindi");
                    uyeler();
                   // Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            frmanasayfa ana = new frmanasayfa();
            ana.Show();
            this.Hide();
        }

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }
    }
}
