using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login.classes
{
    class Connection
    {
        private string dataAccess = System.Configuration.ConfigurationManager.AppSettings["dataAccess"];
        private string dir = System.Configuration.ConfigurationManager.AppSettings["dir"];

        public SqlConnection open()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dir + dataAccess + ";Integrated Security=True;Connect Timeout=30";
            SqlConnection sqlcon = new SqlConnection(connectionString);

            return sqlcon;
        }
    }
}
