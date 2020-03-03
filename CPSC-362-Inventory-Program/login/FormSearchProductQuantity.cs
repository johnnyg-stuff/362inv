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
    public partial class FormSearchProductQuantity : Form
    {

        SqlConnection sqlcon = null;//sql connection variable

        public FormSearchProductQuantity()//constructor
        {
            InitializeComponent();
            Connection open = new Connection();// create a connection object
            this.sqlcon = open.connect();//set sqlcon to the sql connection object returned from the connect function

            sqlcon.Open();//open database
            SqlCommand query = new SqlCommand("SELECT ProductBrand, ProductName FROM Product;", sqlcon);//get product brand and product name from Product entity
            SqlDataReader read = query.ExecuteReader();//execute query and store values to data reader
            while (read.Read())//while reading data from data reader
            {
                comboBox1.Items.Add(read.GetString(0) + " " + read.GetString(1));//add items to combobox1
            }
            read.Close();//close data reader
            sqlcon.Close();//close database;
        }

        private void button1_Click(object sender, EventArgs e)//search product quantity
        {
            SqlCommand query = new SqlCommand("SELECT ProductID FROM Product WHERE ProductBrand + ' ' + ProductName = '" + comboBox1.SelectedItem + "';", sqlcon);//get product id from product brand + product name in combobox1
            if (comboBox1.SelectedIndex > -1)//check if something is selected in combobox1
            {
                sqlcon.Open();//open database
                string output = query.ExecuteScalar().ToString();//set output to value output from executeing the query
                SqlCommand query1 = new SqlCommand("SELECT ProductBrand FROM Product WHERE ProductID = @ProductID", sqlcon);//get product brand from product entity
                query1.Parameters.AddWithValue("@ProductID", output);//set product id to text in value in output
                SqlCommand query2 = new SqlCommand("SELECT ProductName FROM Product WHERE ProductID = @ProductID", sqlcon);//get product name from product entity
                query2.Parameters.AddWithValue("@ProductID", output);//set product id to text in value in output
                SqlCommand query3 = new SqlCommand("SELECT Quantity FROM Product WHERE ProductID = @ProductID", sqlcon);//get quantity from product entity
                query3.Parameters.AddWithValue("@ProductID", output);//set product id to text in value in output
                string brand = query1.ExecuteScalar().ToString();//set brand to value output from executing the query
                string name = query2.ExecuteScalar().ToString();//set name to value output from executing the query
                string quantity = query3.ExecuteScalar().ToString();//set quantity to value output from executing the query
                MessageBox.Show("Product " + output + " " + brand + " " + name + " has " + quantity + " in stock.");//show message box
                sqlcon.Close();//close database
            }
            else
            {
                MessageBox.Show("Product Not Selected.");//show message box
            }
        }

        private void button2_Click(object sender, EventArgs e)//exit search product quantity form
        {
            Close();//close search product quantity form
        }
    }
}
