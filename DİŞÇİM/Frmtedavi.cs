using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DİŞÇİM
{
    public partial class Frmtedavi : Form
    {
        public Frmtedavi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into  TedaviTbl values('" + TedaviAdTb.Text + "','" + TutarTb.Text + "','" + AçıklamaTb.Text + "')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("TEDAVİ KAYDEDİLMİŞTİR");
                uyeler();
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);


            }

        }
        int key;
        private void button5_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Tedaviyi Seçiniz");

            }
            else
            {
                try
                {

                    string query = "Update TedaviTbl  set TAd='" + TedaviAdTb.Text + "',TÜcret='" + TutarTb.Text + "',TAciklama='" + AçıklamaTb.Text + "' where Tid=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Tedavi  işlemi Güncellendi");
                    uyeler();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Tedaviyi Seçiniz");

            }
            else
            {
                try
                {

                    string query = "delete from TedaviTbl where Tid=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Tedavi işlemi Silindi");
                    uyeler();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select *from TedaviTbl  ";
            DataSet ds = Hs.Showhasta(query);
            TedaviDGV.DataSource = ds.Tables[0];

        }
        void Filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select *from TedaviTbl where TAd like'%" + AraTb.Text + "%' ";
            DataSet ds = Hs.Showhasta(query);
            TedaviDGV.DataSource = ds.Tables[0];

        }
        void Reset()
        {
            TedaviAdTb.Text = "";
            TutarTb.Text = "";
            AçıklamaTb.Text = "";
            
        }
        private void Frmtedavi_Load(object sender, EventArgs e)
        {
            uyeler();
            Reset();
        }

        private void TedaviDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TedaviAdTb.Text = TedaviDGV.SelectedRows[0].Cells[1].Value.ToString();
            TutarTb.Text = TedaviDGV.SelectedRows[0].Cells[2].Value.ToString();
            AçıklamaTb.Text = TedaviDGV.SelectedRows[0].Cells[3].Value.ToString();
           
            if (TedaviAdTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TedaviDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
