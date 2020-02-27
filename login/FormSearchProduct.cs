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
    public partial class FormSearchProduct : Form
    {

        SqlConnection sqlcon = null;

        public FormSearchProduct()
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {//search product quantity
            sqlcon.Open();
            SqlCommand query = new SqlCommand("SELECT Quantity FROM Product WHERE ProductID = @ProductID", sqlcon);
            query.Parameters.AddWithValue("@ProductID", textBox1.Text);
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("You did not enter a product ID!");
            }
            else
            {
                string quantity = query.ExecuteScalar().ToString();
                MessageBox.Show("Product " + textBox1.Text + " has " + quantity + " in stock.");
            }
            sqlcon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {//search product location
            sqlcon.Open();
            SqlCommand query = new SqlCommand("SELECT ProductLocation FROM Product WHERE ProductID = @ProductID", sqlcon);
            query.Parameters.AddWithValue("@ProductID", textBox2.Text);
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("You did not enter a product ID!");
            }
            else
            {
                string location = query.ExecuteScalar().ToString();
                MessageBox.Show("Product " + textBox2.Text + " is located at " + location + ".");
            }
            sqlcon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {//exit
            Close();
        }
    }
}
