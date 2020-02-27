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
    public partial class FormDeleteProduct : Form
    {
        SqlConnection sqlcon = null;

        public FormDeleteProduct(FormMain form)
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();
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
        {   //exit
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {//delete
            sqlcon.Open();
            SqlCommand query = new SqlCommand("DELETE FROM Product WHERE productID = @productID", sqlcon);
            query.Parameters.AddWithValue("@productID", textBox1.Text);
            query.ExecuteNonQuery();
            update();
            sqlcon.Close();
            //update gridview in form2
            textBox1.Text = String.Empty;
        }
    }
}
