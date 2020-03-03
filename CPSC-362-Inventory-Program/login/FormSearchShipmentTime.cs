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
    public partial class FormSearchShipmentTime : Form
    {

        SqlConnection sqlcon = null;//sql connection variable

        public FormSearchShipmentTime()//constructor
        {
            InitializeComponent();
            Connection open = new Connection();// create a connection object
            this.sqlcon = open.connect();//set sqlcon to the sql connection object returned from the connect function

            sqlcon.Open();//open database
            SqlCommand query = new SqlCommand("SELECT ShippingID FROM ShippingRecord;", sqlcon);//get shipping id from Shipping Record entity
            SqlDataReader read = query.ExecuteReader();//execute query and store values to data reader
            while (read.Read())//while reading data from data reader
            {
                comboBox1.Items.Add(read.GetString(0));//add items to combobox1
            }
            read.Close();//close data reader
            sqlcon.Close();//close database;
        }

        private void button1_Click(object sender, EventArgs e)//look up expected arrival time
        {
            string message = "";//initilize string 
            if (comboBox1.SelectedIndex > -1)//check if something is selected in combobox1
            {
                sqlcon.Open();//open database
                SqlCommand query1 = new SqlCommand("SELECT ArrivalTime FROM ShippingRecord WHERE ShippingID = @ShippingID;", sqlcon);//get arrival time from shipping record
                query1.Parameters.AddWithValue("@ShippingID", comboBox1.Text);//set shipping id to text in value in combobox1
                SqlCommand query2 = new SqlCommand("SELECT P.ProductName, IP.IncomingQuantity FROM Product AS P, IncomingProduct AS IP WHERE IP.ShippingID = @ShippingID AND IP.ProductID = P.ProductID;", sqlcon);//get product name and incoming quantity from product and incoming product entity
                query2.Parameters.AddWithValue("@ShippingID", comboBox1.Text);//set shipping to text in value in combobox1
                string time = query1.ExecuteScalar().ToString();//set time to value output from executing the query
                SqlDataReader read = query2.ExecuteReader();//execute query and store values to data reader
                while (read.Read())//while reading data from data reader
                {
                    message = message + read.GetString(1) + " cases of " + read.GetString(0) + "\n";//add data to message string
                }
                read.Close();//close data reader
                MessageBox.Show("Shippment " + comboBox1.Text + " is expected to arrive on " + time + " with\n\n" + message + "\n.");//show message box
                sqlcon.Close();//close database
            }
            else
            {
                MessageBox.Show("Shipping ID Not Selected.");//show message box
            }
        }

        private void button3_Click(object sender, EventArgs e)//exit search shipping time form
        {
            Close();//close search shipping time form
        }
    }
}
