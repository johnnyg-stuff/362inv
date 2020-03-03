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
    public partial class FormInsertSupplier : Form
    {

        SqlConnection sqlcon = null;//sql connection variable

        public FormInsertSupplier()//constructor
        {
            InitializeComponent();
            Connection open = new Connection();// create a connection object
            this.sqlcon = open.connect();//set sqlcon to the sql connection object returned from the connect function
            update();
        }

        public void update()
        {
            sqlcon.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Supplier;", sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds, "Supplier");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Supplier";
            sqlcon.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlCommand query = new SqlCommand("INSERT INTO Supplier VALUES (@SupplierID, @SupplierName, @SupplierEmail, @SupplierPhoneNumber);", sqlcon);
            query.Parameters.AddWithValue("@SupplierID", textBox1.Text);
            query.Parameters.AddWithValue("@SupplierName", textBox2.Text);
            query.Parameters.AddWithValue("@SupplierEmail", textBox3.Text);
            query.Parameters.AddWithValue("@SupplierPhoneNumber", textBox4.Text);
            query.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Supplier " + textBox1.Text + " inserted.");
            update();
            //update gridview in form2
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
        }

        private void FormInsertSupplier_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.Supplier' table. You can move, or remove it, as needed.
            this.supplierTableAdapter.Fill(this.loginDataSet.Supplier);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
