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
    public partial class LoginForm : Form
    {

        SqlConnection sqlCon = null;
        public LoginForm()
        {
            InitializeComponent();

            Connection opener = new Connection();
            this.sqlCon = opener.open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void Login_button_Click(object sender, EventArgs e)
        {
            string query = "Select * from Login WHERE username = '" + txtUsername.Text.Trim() + "' and password = '" + txtPassword.Text.Trim() + "'";
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(query, this.sqlCon);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count == 1)
                {
                    Form2 objForm2 = new Form2();
                    this.Hide();
                    objForm2.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username and Password!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
