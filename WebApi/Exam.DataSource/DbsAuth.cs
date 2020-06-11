using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataSource
{
    public class DbsAuth
    {
        public static bool GetAuth(string Name, string NameP)
        {
            var command = new SqlCommand("uspAuth", DbsConnections.SqlBCApp) { CommandType = CommandType.StoredProcedure };

            command.Parameters.Add(DbsSqlParams.Direct("@Name", SqlDbType.VarChar, Name));
            command.Parameters.Add(DbsSqlParams.Direct("@NameP", SqlDbType.VarChar, NameP));

            return DbsSqlOperations.GetInt(command) == 1;
        }
    }
}
