using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Exam.DataSource
{
    internal class DbsConnections
    {
        public static SqlConnection SqlBCApp { get { return new SqlConnection(ConfigurationManager.ConnectionStrings["Sql.BCApp"].ConnectionString); } }
    }
}
