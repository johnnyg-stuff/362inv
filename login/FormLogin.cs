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
    public partial class FormLogin : Form
    {
        SqlConnection sqlcon = null;

        public FormLogin()
        {
            InitializeComponent();
            Connection open = new Connection();
            this.sqlcon = open.connect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void Login_button_Click(object sender, EventArgs e)
        {
            string query = "Select * from Login WHERE username = '" + txtUsername.Text.Trim() + "' and password = '" + txtPassword.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if (dtbl.Rows.Count == 1)
            {
                FormMain objForm2 = new FormMain();
                this.Hide();
                objForm2.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username and Password!");
            }
        }
    }
}
