using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitap_yurdu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Casper\Desktop\KitapYurdu.accdb");
        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Kitaplar", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }
        string durum = "";
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("insert into Kitaplar (KitapAd,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut1.Parameters.AddWithValue("@p1", txtad.Text);
            komut1.Parameters.AddWithValue("@p2", txtyazar.Text);
            komut1.Parameters.AddWithValue("@p3", cmbTur.Text);
            komut1.Parameters.AddWithValue("@p4", txtsayfa.Text);
            komut1.Parameters.AddWithValue("@p5", durum);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap sisteme kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Delete from Kitaplar where Kitapid=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap bilgileri silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Update Kitaplar set KitapAd=@k1,Yazar=@k2,Tur=@k3,Sayfa=@k4,Durum=@k5 where Kitapid=@k6", baglanti);
            komut.Parameters.AddWithValue("@k1", txtad.Text);
            komut.Parameters.AddWithValue("@k2", txtyazar.Text);
            komut.Parameters.AddWithValue("@k3", cmbTur.Text);
            komut.Parameters.AddWithValue("@k4", txtsayfa.Text);
            if (radioButton1.Checked == true)
            {
                komut.Parameters.AddWithValue("@k5", durum);
            }
            if (radioButton2.Checked == true)
            {
                komut.Parameters.AddWithValue("@k5", durum);
            }
            komut.Parameters.AddWithValue("@k6", txtid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }
        private void txtara_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnara_Click(object sender, EventArgs e)
        {           
            OleDbCommand komut = new OleDbCommand("Select *from Kitaplar Where KitapAd=@k1", baglanti);
            komut.Parameters.AddWithValue("@k1", txtara.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void txtara_Leave_1(object sender, EventArgs e)
        {
            if (txtara.Text == "")
            {
                txtara.Text = "Kitap adı giriniz...";
                txtara.ForeColor = Color.Gray;
            }
        }

        private void txtara_Enter(object sender, EventArgs e)
        {
            if (txtara.Text == "Kitap adı giriniz..")
            {
                txtara.Text = "";
                txtara.ForeColor = Color.Black;
            }
        }
    }
}
