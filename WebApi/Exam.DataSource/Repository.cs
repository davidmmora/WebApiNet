using Exam.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataSource
{
    public class Repository
    {
        public User GetUser(string name)
        {
            User item = null;
            SqlCommand cmd = new SqlCommand("dbo.uspGetUserByName", DbsConnections.SqlBCApp);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DbsSqlParams.Direct("@Name", SqlDbType.VarChar, name));

            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                item = new User()
                {
                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    Password = reader.GetString(reader.GetOrdinal("Password"))
                };
            }
            reader.Close();
            return item;
        }
    }
}
