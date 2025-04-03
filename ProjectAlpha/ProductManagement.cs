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
using System.Xml.Linq;

namespace ProjectAlpha
{
    public partial class ProductManagement : Form
    {
        int rowindex = -1;
        public ProductManagement()
        {
            InitializeComponent();
            ProductList();
        }

        public void ProductList()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT P.ID, P.Name, Stock, Price, C.Name, B.Name, Image FROM Product as P JOIN Category as C ON C.ID  = P.CategoryID JOIN Brand as B ON B.ID = P.BrandID";
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Ürün ID");
            dt.Columns.Add("Marka");
            dt.Columns.Add("İsim");
            dt.Columns.Add("Stok Miktarı");
            dt.Columns.Add("Fiyat");
            dt.Columns.Add("Kategori");
            //dt.Columns.Add("Image");
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int stock = reader.GetInt16(2);
                decimal price = reader.GetDecimal(3);
                string categoryName = reader.GetString(4);
                string brandName = reader.GetString(5);
                //string imagename = reader.IsDBNull(6) == false ? reader.GetString(6) : "none";
                ////Image productImg =  Image.FromFile(@"C:\Users\user\Documents\GitHub\ProjectAlpha_E_Commerce\ProjectAlpha\Assets\ProductImages\" + imagename +".jpg");
                dt.Rows.Add(id, brandName, name, stock, price, categoryName/*, productImg*/);
            }
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dataGridView1.ClearSelection();
            rowindex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (rowindex != -1)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
                dataGridView1.Rows[rowindex].Selected = true;
                ProductDetails frm = new ProductDetails(id);
                frm.Show();
            }
        }
    }
}
