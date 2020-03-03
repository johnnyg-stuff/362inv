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
    public partial class FormMain : Form
    {

        SqlConnection sqlcon = null;//sql connection variable

        public FormMain()
        {
            InitializeComponent();
            Connection open = new Connection();//create a connection object
            this.sqlcon = open.connect();//set sqlcon to the sql connection object returned from the connect function
        }

        private void button1_Click(object sender, EventArgs e)//open search product quantity form
        {
            FormSearchProductQuantity form = new FormSearchProductQuantity();//create search product quantity form object
            form.ShowDialog();//show search product quantity form
        }


        private void Button2_Click(object sender, EventArgs e)//open search product location form
        {
            FormSearchProductLocation form = new FormSearchProductLocation();//create search product location form object
            form.ShowDialog();//show search product location form
        }

        private void button3_Click(object sender, EventArgs e)//open search shipment expected arrival time form
        {
            FormSearchShipmentTime form = new FormSearchShipmentTime();//create search expected arrival time form object
            form.ShowDialog();//show seach expected arrival time form
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.loginDataSet.Product);
            dataGridView1.DataSource = Source();//fill grid view with data table from source() function
        }

        private DataTable dt = new DataTable();//data table object
        private DataSet ds = new DataSet();//data set object
        
        public DataTable Source()
        {
            sqlcon.Open();//open database
            SqlCommand cmd = sqlcon.CreateCommand();//create a sql command object
            cmd.CommandText = "SELECT * FROM Product";//set sql command to look for everything in Product entity
            SqlDataAdapter adap = new SqlDataAdapter(cmd);//create a sql data adapter object with sql command
            ds.Clear();//clear data set
            adap.Fill(ds);//fill adapter with dataset
            dt = ds.Tables[0];//set data table to everything in data set index 0
            sqlcon.Close();//close database
            return dt;//return data table
        }

        private void Form3_UpdateEventHandler(object sender, FormInsertProduct.UpdateEventArgs args)
        {
            dataGridView1.DataSource = Source();//fill grid view with data table from source() function
        }


        private void FormUpdateProduct_UpdateEventHandler(object sender, FormUpdateProduct.UpdateEventArgs args)
        {
            dataGridView1.DataSource = Source();//fill grid view with data table from source() function
        }
        private void Form4_UpdateEventHandler(object sender, FormDeleteProduct.UpdateEventArgs args)
        {
            dataGridView1.DataSource = Source();//fill grid view with data table from source() function
        }

        private void insertProductToolStripMenuItem_Click(object sender, EventArgs e)//open insert product form 
        {
            FormInsertProduct form = new FormInsertProduct(this);//create insert product form object
            form.UpdateEventHandler += Form3_UpdateEventHandler;//update gridview when updateventhandler is called in insert product form object
            form.ShowDialog();//show insert product form 
        }

        private void deleteProductToolStripMenuItem_Click(object sender, EventArgs e)//open delete product form
        {
            FormDeleteProduct form = new FormDeleteProduct(this);//create delete product form object
            form.UpdateEventHandler += Form4_UpdateEventHandler;//update gridview when updateeventhandler is called in delete product form object
            form.ShowDialog();//show delete product form
        }

        private void insertSupplierToolStripMenuItem_Click(object sender, EventArgs e)//open insert supplier form
        {
            FormInsertSupplier form = new FormInsertSupplier();//create insert supplier form object
            form.ShowDialog();//show insert supplier form
        }

        private void deleteSupplierToolStripMenuItem_Click(object sender, EventArgs e)//open delete supplier form
        {
            FormDeleteSupplier form = new FormDeleteSupplier();//create delete supplier form object
            form.ShowDialog();//show delete supplier form 
        }

        private void updateProductToolStripMenuItem_Click(object sender, EventArgs e)//open update product form
        {
            FormUpdateProduct form = new FormUpdateProduct(this);//create update product form
            form.UpdateEventHandler += FormUpdateProduct_UpdateEventHandler;//update gridview when updateeventhandler is called in delete product form object
            form.ShowDialog();//show update product form
        }

        private void updateSupplierToolStripMenuItem_Click(object sender, EventArgs e)//open update supplier form
        {
            FormUpdateSupplier form = new FormUpdateSupplier();//create update supplier form
            form.ShowDialog();//show update supplier form
        }
    }
}
