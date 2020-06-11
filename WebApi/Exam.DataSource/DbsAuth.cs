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
        public static bool GetAuth(int? userId, string module, string action)
        {
            var command = new SqlCommand("uspAuth", DbsConnections.SqlBCApp) { CommandType = CommandType.StoredProcedure };

            command.Parameters.Add(DbsSqlParams.Direct("@MODULE_PARAM", SqlDbType.VarChar, module));
            command.Parameters.Add(DbsSqlParams.Direct("@ACTION_PARAM", SqlDbType.VarChar, action));
            command.Parameters.Add(DbsSqlParams.Direct("@USERID_PARAM", SqlDbType.Int, userId));

            return DbsSqlOperations.GetInt(command) == 1;
        }
    }
}
