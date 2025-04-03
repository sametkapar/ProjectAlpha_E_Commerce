using ProjectAlpha.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAlpha
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginScreen lgnscrn = new LoginScreen();
            lgnscrn.ShowDialog();
            if (LoginUser.user.ManagerID != 1)
            {
                CI_TS.Enabled = false;
            }
            toolStripStatusLabel1.Text = "Kullanıcı: " + LoginUser.user.Username + "Rol: " + LoginUser.user.ManagerType;
        }
        private void TSMI_urunList_Click(object sender, EventArgs e)
        {
            Form[] acikFormlar = this.MdiChildren;
            bool acikMi = false;
            foreach (Form form in acikFormlar)
            {
                if (form.GetType() == typeof(ProductManagement))
                {
                    acikMi = true;
                    form.Activate();
                }
            }
            if (acikMi == false)
            {
                ProductManagement frm = new ProductManagement();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void TSMI_urunEkle_Click(object sender, EventArgs e)
        {
            Form[] acikFormlar = this.MdiChildren;
            bool acikMi = false;
            foreach (Form form in acikFormlar)
            {
                if (form.GetType() == typeof(ProductAdd))
                {
                    acikMi = true;
                    form.Activate();
                }
            }
            if (acikMi == false)
            {
                ProductAdd frm = new ProductAdd();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void TSMI_categoryAdd_Click(object sender, EventArgs e)
        {
            CategoryAdd frm = new CategoryAdd();
            frm.Show();
        }

        private void TSMI_kategoriList_Click(object sender, EventArgs e)
        {
            CategoryManagement frm = new CategoryManagement();
            frm.Show();
        }
    }
}
