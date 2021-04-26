using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RubikStore.Models
{
    public class DBConnection
    {
        string strCon;

        public DBConnection()
        {
            strCon = ConfigurationManager.ConnectionStrings["RubikConnectionString"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(strCon);
        }
    }
}