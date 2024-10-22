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
    public partial class Frmhasta : Form
    {
        public Frmhasta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into HastaTbl values('" + HAdSoyadTb.Text + "','" + HTelefonTb.Text + "','" + AdresTb.Text + "','" + HDogumTarih.Text + "','" + HCinsiyetCb.SelectedItem.ToString() + "','" + AlerjiTb.Text + "')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("HASTA KAYDEDİLMİŞTİR");
                uyeler();
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);


            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select *from HastaTbl ";
            DataSet ds = Hs.Showhasta(query);
            HastaDGV.DataSource = ds.Tables[0];

        }
        void Filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select *from HastaTbl where HAd like'%"+AraTb.Text+"%' ";
            DataSet ds = Hs.Showhasta(query);
            HastaDGV.DataSource = ds.Tables[0];

        }
        void Reset()
        {
            HAdSoyadTb.Text = "";
            HTelefonTb.Text = "";
            AdresTb.Text = "";
            HDogumTarih.Text = "";
            HCinsiyetCb.SelectedItem = "";
            AlerjiTb.Text = "";
        }
        private void Frmhasta_Load(object sender, EventArgs e)
        {
            uyeler();
            Reset();

        }
        int key = 0;
        private void HastaDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HAdSoyadTb.Text = HastaDGV.SelectedRows[0].Cells[1].Value.ToString();
            HTelefonTb.Text = HastaDGV.SelectedRows[0].Cells[2].Value.ToString();
            AdresTb.Text = HastaDGV.SelectedRows[0].Cells[3].Value.ToString();
            HDogumTarih.Text = HastaDGV.SelectedRows[0].Cells[4].Value.ToString();
            HCinsiyetCb.SelectedItem = HastaDGV.SelectedRows[0].Cells[5].Value.ToString();
            AlerjiTb.Text = HastaDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (HAdSoyadTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(HastaDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key==0)
            {
                MessageBox.Show("Silinecek Hastayı Seçiniz");

            }
            else
            {
                try
                {

                    string query = "delete from HastaTbl where Hid=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Hasta Silindi");
                    uyeler();
                    Reset();

                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                
            }
          
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Hastayı Seçiniz");

            }
            else
            {
                try
                {

                    string query = "Update HastaTbl  set HAd='"+HAdSoyadTb.Text+"',HTelefon='"+HTelefonTb.Text+"',HAdres='"+AdresTb.Text+"',HDTarih='"+HDogumTarih.Text+"',HCinsiyet='"+HCinsiyetCb.SelectedItem.ToString()+"',HAlerji='"+AlerjiTb.Text+"' where Hid=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Hasta Güncellendi");
                    uyeler();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void label10_Click(object sender, EventArgs e)
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
