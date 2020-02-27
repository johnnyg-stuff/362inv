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
    public partial class FormInsertProduct : Form
    {
        SqlConnection sqlcon = null;
       
        public FormInsertProduct(FormMain form2)
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();

            sqlcon.Open();
            SqlCommand query = new SqlCommand("SELECT SupplierName FROM Supplier;", sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(query);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i][0]);
            }
            sqlcon.Close();
        }

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void update()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        private void Button2_Click(object sender, EventArgs e)
        {   //Exit
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        { //insert
            sqlcon.Open();
            SqlCommand query = new SqlCommand("INSERT INTO Product VALUES (@productID, @productBrand, @productName, @price, @quantity, @expirationDate, @productLocation, @supplierID);", sqlcon);
            query.Parameters.AddWithValue("@productID", textBox1.Text);
            query.Parameters.AddWithValue("@productBrand", textBox2.Text);
            query.Parameters.AddWithValue("@productName", textBox3.Text);
            query.Parameters.AddWithValue("@price", textBox4.Text);
            query.Parameters.AddWithValue("@quantity", textBox5.Text);
            query.Parameters.AddWithValue("@expirationDate", textBox6.Text);
            query.Parameters.AddWithValue("@productLocation", textBox7.Text);
            SqlCommand query1 = new SqlCommand("SELECT SupplierID FROM Supplier WHERE SupplierName = '" + comboBox1.SelectedItem + "';", sqlcon);
            SqlDataReader read = query1.ExecuteReader();
            read.Read();
            string output = read.GetString(0);
            read.Close();
            query.Parameters.AddWithValue("@SupplierID", output);
            //try
            //{
                query.ExecuteNonQuery();
            //}
            //catch
            //{
                //MessageBox.Show("This Product ID already exist!");
            //}
            update();
            sqlcon.Close();
            //update gridview in form2
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox7.Text = String.Empty;
            comboBox1.SelectedIndex = -1;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.loginDataSet.Product);
        }
    }
}
