using ProjectAlpha.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAlpha
{
    public partial class LoginScreen : Form
    {
        bool islogin = false;
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_username.Text) && !string.IsNullOrEmpty(tb_password.Text))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT ID, Name, Surname, Mail, Password, Name+' '+Surname, Username FROM Manager Where Username = @un AND Password = @pw";
                    cmd.Parameters.AddWithValue("@un", tb_username.Text);
                    cmd.Parameters.AddWithValue("@pw", tb_password.Text);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    User u = null;
                    while (rdr.Read())
                    {
                        u = new User();
                        u.ID = rdr.GetInt32(0);
                        u.Name = rdr.GetString(1);
                        u.Surname = rdr.GetString(2);
                        u.Email = rdr.GetString(3);
                        u.Password = rdr.GetString(4);
                        u.Fullname = rdr.GetString(5);
                        u.Username = rdr.GetString(6);
                    }
                    if (u != null)
                    {
                        LoginUser.user = u;
                        islogin = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı ve şifre boş bırakılamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (islogin == false)
            {
                Application.Exit();
            }
        }
    }
}
