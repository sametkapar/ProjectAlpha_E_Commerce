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
    public partial class ProductDetails : Form
    {
        public ProductDetails()
        {
            InitializeComponent();
        }
        public ProductDetails(int id)
        {
            InitializeComponent();
            ProductDetail(id);
        }
        public void ProductDetail(int id)
        {

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT P.ID, P.Name, Stock, Price, C.Name, B.Name, P.Description FROM Product as P JOIN Category as C ON C.ID  = P.CategoryID JOIN Brand as B ON B.ID = P.BrandID WHERE P.ID = @id";
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Product p;
            while (reader.Read())
            {
                p = new Product();
                p.ID = reader.GetInt32(0);
                p.Name = reader.GetString(1);
                p.Stock = reader.GetInt16(2);
                p.Price = reader.GetDecimal(3);
                p.CategoryName = reader.GetString(4);
                p.BrandName = reader.GetString(5);
                p.Description = reader.GetString(6);
            }
        }
    }
}
