using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_app.Cls
{
    internal class db
    {
        SqlConnection connect;
        public db()
        {

            string query_conn = "Data Source=102.209.3.101;Initial Catalog=Mafaz_Live;User ID=sa;Password=B1admin;";
           

            connect = new SqlConnection(query_conn);

        }

        public SqlConnection getconn
        //public SqlConnection getconn
        {
            get
            {
                return connect;
            }
        }

        // To open the connection
        public void openconn()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        // To close the connection
        public void closeconn()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }
    }
}
