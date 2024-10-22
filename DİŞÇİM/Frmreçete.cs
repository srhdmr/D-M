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
    public partial class Frmreçete : Form
    {
        public Frmreçete()
        {
            InitializeComponent();
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
            HastaCb.ValueMember = "HAd";
            HastaCb.DataSource = dt;
            baglanti.Close();


        }
        private void filltedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select *from RandevuTbl where Hasta='" + HastaCb.SelectedValue.ToString() + "'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TedaviTb.Text = dr["Tedavi"].ToString();
            }
            baglanti.Close();


        }
        private void fillprice()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select *from TedaviTbl where TAd='" + TedaviTb.Text + "'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TutarTb.Text = dr["TÜcret"].ToString();
            }
            baglanti.Close();


        }
        private void Frmreçete_Load(object sender, EventArgs e)
        {
            fillHasta();
            Reset();
            uyeler();
        }

        private void HastaCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filltedavi();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            frmanasayfa ana = new frmanasayfa();
            ana.Show();
            this.Hide();
        }

        private void TutarTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void TedaviTb_TextChanged(object sender, EventArgs e)
        {
            fillprice();
        }
        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select *from ReçeteTbl  ";
            DataSet ds = Hs.Showhasta(query);
            ReçeteDGV.DataSource = ds.Tables[0];

        }
        void Filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select *from ReçeteTbl where HasAd like'%" + ARATB.Text + "%' ";
            DataSet ds = Hs.Showhasta(query);
            ReçeteDGV.DataSource = ds.Tables[0];

        }
        void Reset()
        {
            HastaCb.SelectedItem = "";
            TedaviTb.Text = "";
            TutarTb.Text = "";
            İlaçlarTb.Text = "";
            MiktarTb.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into  ReçeteTbl values('" + HastaCb.SelectedValue.ToString() + "','" + TedaviTb.Text + "'," + TutarTb.Text + ",'" + İlaçlarTb.Text + "'," + MiktarTb.Text + ")";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("REÇETE KAYDEDİLMİŞTİR");
                uyeler();
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);


            }
        }
        int key = 0;
        private void ReçeteDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HastaCb.Text = ReçeteDGV.SelectedRows[0].Cells[1].Value.ToString();
            TedaviTb.Text = ReçeteDGV.SelectedRows[0].Cells[2].Value.ToString();
            İlaçlarTb.Text = ReçeteDGV.SelectedRows[0].Cells[3].Value.ToString();
            TutarTb.Text = ReçeteDGV.SelectedRows[0].Cells[4].Value.ToString();
            MiktarTb.Text = ReçeteDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (TedaviTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ReçeteDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Reçeteyi Seçiniz");

            }
            else
            {
                try
                {

                    string query = "delete from ReçeteTbl where Recid=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Reçete Silindi");
                    uyeler();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ARATB_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

      
    }
}
    

