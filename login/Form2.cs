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

namespace login
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {   //search
            Form9 form = new Form9();
            form.ShowDialog();
        }


        private void Button2_Click(object sender, EventArgs e)
        { // refresh

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.loginDataSet.Product);
            dataGridView1.DataSource = Source();
        }

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        
        public DataTable Source()
        {
            string dataAccess = System.Configuration.ConfigurationManager.AppSettings["dataAccess"];
            string dir = System.Configuration.ConfigurationManager.AppSettings["dir"];
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dir + dataAccess + ";Integrated Security=True;Connect Timeout=30";
            SqlConnection sqlcon = new SqlConnection(connectionString);
            sqlcon.Open();
            SqlCommand cmd = sqlcon.CreateCommand();
            cmd.CommandText = "SELECT * FROM Product";
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            ds.Clear();
            adap.Fill(ds);
            dt = ds.Tables[0];
            sqlcon.Close();
            return dt;
        }

        private void Form3_UpdateEventHandler(object sender, Form3.UpdateEventArgs args)
        {
            dataGridView1.DataSource = Source();
        }

        private void Form4_UpdateEventHandler(object sender, Form4.UpdateEventArgs args)
        {
            dataGridView1.DataSource = Source();
        }

        /*
        private void Form5_UpdateEventHandler(object sender, Form5.UpdateEventArgs args)
        {
            dataGridView1.DataSource = Source();
        }
        */

        private void insertProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3(this);
            form.UpdateEventHandler += Form3_UpdateEventHandler;
            form.ShowDialog();
        }

        private void deleteProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4(this);
            form.UpdateEventHandler += Form4_UpdateEventHandler;
            form.ShowDialog();
        }

        private void insertSupplierToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            //form.UpdateEventHandler += Form5_UpdateEventHandler;
            form.ShowDialog();
        }

        private void deleteSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            form.ShowDialog();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void createNewAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateAccountForm form = new CreateAccountForm();
            form.ShowDialog();
        }
    }
}
