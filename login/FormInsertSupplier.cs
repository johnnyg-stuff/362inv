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

        SqlConnection sqlcon = null;

        public FormInsertSupplier()
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();
        }

        /*
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
        */

        private void Button1_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlCommand query = new SqlCommand("INSERT INTO Supplier VALUES (@supplierID, @supplierName, @supplierEmail, @supplierPhoneNumber);", sqlcon);
            query.Parameters.AddWithValue("@supplierID", textBox1.Text);
            query.Parameters.AddWithValue("@supplierName", textBox2.Text);
            query.Parameters.AddWithValue("@supplierEmail", textBox3.Text);
            query.Parameters.AddWithValue("@supplierPhoneNumber", textBox4.Text);
            query.ExecuteNonQuery();
            //update();
            sqlcon.Close();
            //update gridview in form2
            Close();
        }

    }
}
