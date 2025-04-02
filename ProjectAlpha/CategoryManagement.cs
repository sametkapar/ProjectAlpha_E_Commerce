using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAlpha
{
    public partial class CategoryManagement : Form
    {
        int rowindex = -1;
        public CategoryManagement()
        {
            InitializeComponent();
            CategoryList();
        }
        public void CategoryList()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectAlpha_DB;Integrated Security=true");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID, Name, isActive, isDeleted FROM Category WHERE isDeleted = 0";
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("İsim");
            dt.Columns.Add("Aktif mi?");
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                bool isActive = reader.GetBoolean(2);
                bool isDeleted = reader.GetBoolean(3);
                dt.Rows.Add(id, name, isActive ? "Evet" : "Hayır");
            }
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void btn_categoryAdd_Click(object sender, EventArgs e)
        {
            CategoryAdd frm = new CategoryAdd();
            frm.Show();

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
      
            dataGridView1.ClearSelection();
            rowindex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (rowindex != -1)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
                dataGridView1.Rows[rowindex].Selected = true;
                CategoryAdd frm = new CategoryAdd(id);
                frm.Show();
                this.Close();
            }
        }
    }
}
