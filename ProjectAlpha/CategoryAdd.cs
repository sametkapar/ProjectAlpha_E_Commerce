using ProjectAlpha.Model;
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

namespace ProjectAlpha
{
    public partial class CategoryAdd : Form
    {
        public CategoryAdd()
        {
            InitializeComponent();
        }
        public CategoryAdd(int id)
        {
            InitializeComponent();
            CategoryShow(id);
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_categoryAdd_Click(object sender, EventArgs e)
        {
            if(btn_categoryAdd.Text == "EKLE")
            {
                SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true");
                SqlCommand cmd = con.CreateCommand();
                try
                {
                    // Kategori adının databasede olup olmadığını kontrol ediliyor
                    cmd.CommandText = "SELECT COUNT(*) FROM Category WHERE Name = @name";
                    cmd.Parameters.AddWithValue("@name", tb_name.Text);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Bu kategori adı zaten mevcut.");
                        return;
                    }
                    // Kategori ekleme işlemi
                    cmd.CommandText = "INSERT INTO Category (Name, isActive, isDeleted) VALUES (@name, @ia, @isd)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@name", tb_name.Text);
                    cmd.Parameters.AddWithValue("@ia", rb_aktifmi.Checked);
                    bool isDeleted = false;
                    cmd.Parameters.AddWithValue("@isd", isDeleted);
                    cmd.ExecuteNonQuery();
                    if (MessageBox.Show("Katergori başarıyla eklendi", "Başarılı") == DialogResult.OK)
                    {
                        this.Close();
                        CategoryManagement frm = new CategoryManagement();
                        frm.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kategori eklenirken bir hata oluştu. Hata: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            if (btn_categoryAdd.Text == "GÜNCELLE")
            {
                SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true");
                SqlCommand cmd = con.CreateCommand();
                
                try
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Category WHERE Name = @name";
                    cmd.Parameters.AddWithValue("@name", tb_name.Text);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Bu kategori adı zaten mevcut.");
                        return;
                    }
                    cmd.CommandText = "UPDATE Category SET Name=@name, isActive=@ia WHERE ID=@id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@name", tb_name.Text);
                    cmd.Parameters.AddWithValue("@ia", rb_aktifmi.Checked);
                    cmd.Parameters.AddWithValue("@id", tb_ID.Text);
                    cmd.ExecuteNonQuery();
                    if (MessageBox.Show("Katergori başarıyla güncelledi", "Başarılı") == DialogResult.OK)
                    {
                        this.Close();
                        CategoryManagement frm = new CategoryManagement();
                        frm.Show();
                    }


                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Kategori güncellenirken bir hata oluştu. Hata: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

        }

        public void CategoryShow(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID, Name, isActive FROM Category WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    tb_ID.Text = rdr.GetInt32(0).ToString();
                    tb_name.Text = rdr.GetString(1);
                    rb_aktifmi.Checked = rdr.GetBoolean(2);
         
                }
                btn_categoryAdd.Text = "GÜNCELLE";
            }
            catch
            {
                MessageBox.Show("Kategori getirirken bir hata oluştu", "Hata Var");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
