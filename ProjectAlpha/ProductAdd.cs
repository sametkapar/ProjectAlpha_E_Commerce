
using ProjectAlpha.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProjectAlpha
{
    public partial class ProductAdd : Form
    {
        FileInfo fi;
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
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                fi = new FileInfo(path);
                //Dosya boyutu ve image resize yapılacak
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                pb_productImage.Image = Image.FromFile(path);
            }
        }
        private void btn_urunEkle_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Product (Name, Description, Price, Stock, CategoryID, BrandID, Image, isActive, isDeleted) VALUES (@name, @desc, @price, @stock, @cID, @bID, @img, @iA, @idlt)";
            cmd.Parameters.AddWithValue("@name", tb_name.Text);
            cmd.Parameters.AddWithValue("@desc", tb_description.Text);
            cmd.Parameters.AddWithValue("@price", tb_price.Text);
            cmd.Parameters.AddWithValue("@stock", tb_stock.Text);
            cmd.Parameters.AddWithValue("@cID", cb_categoriesName.SelectedValue);
            cmd.Parameters.AddWithValue("@bID", cb_brandsName.SelectedValue);
            string extension = fi.Extension;
            string databaseSaveName = Guid.NewGuid().ToString();
            string newImgName = databaseSaveName + extension;
            cmd.Parameters.AddWithValue("@img", databaseSaveName);
            cmd.Parameters.AddWithValue("@iA", rb_satistami.Checked);
            cmd.Parameters.AddWithValue("@idlt", false);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                if (MessageBox.Show("Ürün Başarıyla Eklenmiştir", "Başarılı") == DialogResult.OK)
                {
                    this.Close();
                    ProductManagement frm = new ProductManagement();
                    frm.Show();
                }
            }
            catch
            {
                MessageBox.Show("Ürün ekleme işleminde bir hata ile karşılaşıldı", "Hata");
            }
            finally
            {
                con.Close();
            }
            string directory = @"C:\Users\user\Documents\GitHub\ProjectAlpha_E_Commerce\ProjectAlpha\Assets\ProductImages\"; // Resimlerin kaydedileceği dizin
            string save = Path.Combine(directory, newImgName);
            pb_productImage.Image.Save(save);

        }
    }
}
