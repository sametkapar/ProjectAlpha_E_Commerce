using ProjectAlpha.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            toolStripStatusLabel1.Text ="Kullanıcı: " +  LoginUser.user.Username + "Rol: " + LoginUser.user.ManagerType;

        }
    }
}
