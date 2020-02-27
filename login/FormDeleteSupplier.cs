using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using login.classes;

namespace login
{
    public partial class FormDeleteSupplier : Form
    {
        SqlConnection sqlcon = null;

        public FormDeleteSupplier()
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlCommand query = new SqlCommand("DELETE FROM Supplier WHERE SupplierID = @SupplierID", sqlcon);
            query.Parameters.AddWithValue("@SupplierID", textBox1.Text);
            query.ExecuteNonQuery();
            //update();
            sqlcon.Close();
            //update gridview in form2
            textBox1.Text = String.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
