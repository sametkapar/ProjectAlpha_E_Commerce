
using ProjectAlpha.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAlpha
{
    public partial class ProductAdd : Form
    {
        string path = "";
        public ProductAdd()
        {
            InitializeComponent();
            CategoryList();
            BrandList();
        }
        public void CategoryList()
        {
            List<Category> categories = new List<Category>();
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID, Name FROM Category";
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Category c = new Category();
                c.ID = rdr.GetInt32(0);
                c.Name = rdr.GetString(1);
                categories.Add(c);
            }
            con.Close();
            cb_categoriesName.Items.Clear();
            cb_categoriesName.DataSource = categories;
            cb_categoriesName.ValueMember = "ID";
            cb_categoriesName.DisplayMember = "Name";

        }
        public void BrandList()
        {
            List<Brand> brands = new List<Brand>();
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID, Name FROM Brand";
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Brand b = new Brand();
                b.ID = rdr.GetInt32(0);
                b.Name = rdr.GetString(1);
                brands.Add(b);
            }
            con.Close();
            cb_brandsName.Items.Clear();
            cb_brandsName.DataSource = brands;
            cb_brandsName.ValueMember = "ID";
            cb_brandsName.DisplayMember = "Name";

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_imageLoad_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = openFileDialog1.ShowDialog();
            path = folderBrowserDialog1.SelectedPath;


            FileInfo fi = new FileInfo(path);
            pb_productImage.ImageLocation = path;
            
        }
    }
}
