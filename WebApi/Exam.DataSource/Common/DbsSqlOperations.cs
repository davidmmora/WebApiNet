using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataSource
{
    internal class DbsSqlOperations : IDisposable
    {
        #region Constructor

        public DbsSqlOperations()
        {
            sqlRead = null;
            Command = null;
            sqlTransaction = null;
            Connection = null;
        }

        public DbsSqlOperations(SqlCommand sCommand)
        {
            Command = sCommand;
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Obtiene o establece el objeto SQLCommand que será ejecutado
        /// </summary>
        public SqlCommand Command { get; set; }

        /// <summary>
        /// Obtiene acceso al objeto SqlData Reader en curso
        /// </summary>
        private SqlDataReader sqlRead { get; set; }

        public SqlConnection Connection { get; set; }

        private SqlTransaction sqlTransaction { get; set; }

        #endregion

        #region Métodos

        /// <summary>
        /// Abre la propiedad Connection y crea una nueva transacción
        /// </summary>
        public void ConnectionOpen()
        {
            Exception ex = null;
            try
            {
                Connection.Open();
                sqlTransaction = Connection.BeginTransaction();
                if (Connection.State != ConnectionState.Open)
                {
                    throw new Exception("La conexión proporcionada no se pudo abrir")
                    {
                        Source = "Operaciones en Sql - Abrir Conexión"
                    };
                }
            }
            catch (Exception err)
            {
                ex = new Exception(err.Message);
            }
            if (ex != null)
                throw ex;
            else
            {
                return;
            }

        }

        /// <summary>
        /// Confirma la transacción y cierra la conexión de la propiedad Connection
        /// </summary>
        public void ConnectionClose()
        {
            Exception ex = null;
            try
            {
                sqlTransaction.Commit();
                Connection.Close();
                if (Connection.State != ConnectionState.Closed)
                {
                    throw new Exception("La conexión proporcionada no se pudo cerrar")
                    {
                        Source = "Operaciones en Sql - cerrar Conexión"
                    };
                }
            }
            catch (Exception err)
            {
                ex = new Exception(err.Message);
            }
            if (ex != null)
                throw ex;
            else
            {
                return;
            }

        }

        /// <summary>
        /// Ejecuta la regresión de la transacción iniciada hasta el punto de arranque y cierra la conexión
        /// </summary>
        public void ConnectionRollBack()
        {
            Exception ex = null;
            try
            {
                sqlTransaction.Rollback();
                Connection.Close();
                if (Connection.State != ConnectionState.Closed)
                {
                    throw new Exception("La conexión proporcionada no se pudo cerrar")
                    {
                        Source = "Operaciones en Sql - cerrar Conexión"
                    };
                }
            }
            catch (Exception err)
            {
                ex = new Exception(err.Message);
            }
            if (ex != null)
                throw ex;
            else
            {
                return;
            }

        }

        /// <summary>
        /// Obtiene el objeto SQLDataReader para leer el siguiente registro
        /// </summary>
        public SqlDataReader Reader { get { return sqlRead; } }

        /// <summary>
        /// Obtiene un objeto DataTable a partir de una consulta
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            if (Command == null)
            {
                throw new Exception("El objeto Command necesario para esta ejecución no está inicializado");
            }
            Exception ex = null;
            DataTable dTable = new DataTable();
            try
            {
                using (SqlDataAdapter Adapter = new SqlDataAdapter(Command))
                {
                    Adapter.Fill(dTable);
                }
            }
            catch (Exception err)
            {
                ex = new Exception(err.Message);
            }
            finally
            {
                if (Command.Connection.State == ConnectionState.Open)
                {
                    Command.Connection.Close();
                }
            }
            if (ex != null)
                throw ex;
            else
                return dTable;
        }

        /// <summary>
        /// Obtiene un objeto DataTable a partir de una consulta
        /// </summary>
        /// <param name="sCommand">Comando SQL previamente llenado con su conexxión, parámetros y nombre</param>
        /// <returns></returns>
        public static DataTable GetDataTable(SqlCommand sCommand)
        {
            using (sCommand)
            {
                if (sCommand == null)
                {
                    throw new Exception("El objeto Command necesario para esta ejecución no está inicializado");
                }
                Exception ex = null;
                SqlDataAdapter Adapter;
                DataTable dTable = new DataTable();
                try
                {
                    Adapter = new SqlDataAdapter(sCommand);
                    Adapter.Fill(dTable);
                }
                catch (Exception err)
                {
                    ex = new Exception(err.Message);
                }
                finally
                {
                    if (sCommand.Connection.State == ConnectionState.Open)
                    {
                        sCommand.Connection.Close();
                    }
                }
                if (ex != null)
                    throw ex;
                else
                    return dTable;
            }
        }

        /// <summary>
        /// Devuelve el valor de la primera columna de la primera fila de la tabla obtenida del comando
        /// </summary>
        /// <param name="sCommand">Comando inicializado</param>
        /// <returns></returns>
        public int GetInt()
        {
            DataTable dtSource = GetDataTable(Command);
            if (dtSource.Rows.Count > 0)
            {
                return Convert.ToInt32(dtSource.Rows[0][0]);
            }
            else
            {
                throw new Exception("El comando no devolvió algún valor");
            }
        }

        /// <summary>
        /// Devuelve el valor de la primera columna de la primera fila de la tabla obtenida del comando
        /// </summary>
        /// <param name="sCommand">Comando inicializado</param>
        /// <returns></returns>
        public static int GetInt(SqlCommand sCommand)
        {
            using (sCommand)
            {
                DataTable dtSource = GetDataTable(sCommand);
                if (dtSource.Rows.Count > 0)
                {
                    return Convert.ToInt32(dtSource.Rows[0][0]);
                }
                else
                {
                    throw new Exception("El comando no devolvió algún valor");
                }
            }
        }

        /// <summary>
        /// Obtiene un objeto DataSet a partir de una consulta
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            if (Command == null)
            {
                throw new Exception("El objeto Command necesario para esta ejecución no está inicializado");
            }
            Exception ex = null;
            SqlDataAdapter Adapter;
            DataSet dTable = new DataSet();
            try
            {
                Adapter = new SqlDataAdapter(Command);
                Adapter.Fill(dTable);
            }
            catch (Exception err)
            {
                ex = new Exception(err.Message);
            }
            finally
            {
                if (Command.Connection.State == ConnectionState.Open)
                {
                    Command.Connection.Close();
                }
            }
            if (ex != null)
                throw ex;
            else
                return dTable;
        }

        /// <summary>
        /// Obtiene un objeto DataSet a partir de una consulta
        /// </summary>
        /// <param name="sCommand">Comando SQL previamente llenado con su conexxión, parámetros y nombre</param>
        /// <returns></returns>
        public static DataSet GetDataSet(SqlCommand sCommand)
        {
            using (sCommand)
            {
                if (sCommand == null)
                {
                    throw new Exception("El objeto Command necesario para esta ejecución no está inicializado");
                }
                Exception ex = null;
                ;
                DataSet dTable = new DataSet();
                try
                {
                    using (SqlDataAdapter Adapter = new SqlDataAdapter(sCommand))
                    {
                        Adapter.Fill(dTable);
                    }
                }
                catch (Exception err)
                {
                    ex = new Exception(err.Message);
                }
                finally
                {
                    if (sCommand.Connection.State == ConnectionState.Open)
                    {
                        sCommand.Connection.Close();
                    }
                }
                if (ex != null)
                    throw ex;
                else
                    return dTable;
            }
        }

        /// <summary>
        /// Obtiene el número de registros afectados después de una consulta
        /// </summary>
        /// <returns></returns>
        public int ExecuteScalar()
        {
            if (Command == null)
            {
                throw new Exception("El objeto Command necesario para esta ejecución no está inicializado");
            }
            Exception ex = null;
            int response = 0;
            if (Connection == null)
            {
                try
                {
                    Command.Connection.Open();
                    response = Command.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    ex = new Exception(err.Message);
                }
                finally
                {
                    Command.Connection.Close();
                }
                if (ex != null)
                    throw ex;
                else
                    return response;
            }
            else
            {
                try
                {
                    Command.Connection = Connection;
                    Command.Transaction = sqlTransaction;
                    response = Command.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    ex = new Exception(err.Message);
                }
                if (ex != null)
                    throw ex;
                else
                    return response;
            }
        }

        /// <summary>
        /// Obtiene el número de registros afectados después de una consulta
        /// </summary>
        /// <param name="sCommand">Comando SQL previamente llenado con su conexxión, parámetros y nombre</param>
        /// <returns></returns>
        public static int ExecuteScalar(SqlCommand sCommand)
        {
            using (sCommand)
            {
                if (sCommand == null)
                {
                    throw new Exception("El objeto Command necesario para esta ejecución no está inicializado");
                }
                Exception ex = null;
                int response = 0;
                try
                {
                    sCommand.Connection.Open();
                    response = sCommand.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    ex = new Exception(err.Message);
                }
                finally
                {
                    sCommand.Connection.Close();
                }
                if (ex != null)
                    throw ex;
                else
                    return response;
            }
        }

        /// <summary>
        /// Obtiene el número de registros afectados después de una consulta
        /// </summary>
        /// <param name="sCommand">Comando SQL previamente llenado con su conexxión, parámetros y nombre</param>
        /// <returns></returns>
        public static int ExecuteScalar(ref SqlCommand sCommand)
        {
            using (sCommand)
            {
                if (sCommand == null)
                {
                    throw new Exception("El objeto Command necesario para esta ejecución no está inicializado");
                }
                Exception ex = null;
                int response = 0;
                try
                {
                    sCommand.Connection.Open();
                    response = sCommand.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    ex = new Exception(err.Message);
                }
                finally
                {
                    sCommand.Connection.Close();
                }
                if (ex != null)
                    throw ex;
                else
                    return response;
            }
        }

        /// <summary>
        /// Avanza al registro siguiente del objeto interno SQlDataReader, si la conexión no está abierta el objeto SQLDataReader se creará y avanzará al siguiente registro,
        /// si el objeto SQLDataReader no tiene más registros la conexión se cerrará
        /// </summary>
        /// <returns></returns>
        public Boolean Read()
        {
            if (Command == null)
            {
                throw new Exception("El objeto Command necesario para esta ejecución no está inicializado");
            }

            Exception ex = null;
            Boolean response = false;
            try
            {
                if (Command.Connection.State == ConnectionState.Closed)
                {
                    Command.Connection.Open();
                    sqlRead = Command.ExecuteReader();
                }
                if (sqlRead.HasRows)
                {
                    response = sqlRead.Read();
                }
            }
            catch (Exception err)
            {
                ex = new Exception(err.Message);
            }
            finally
            {
                if (ex != null || !response)
                {
                    if (sqlRead != null)
                    {
                        sqlRead.Dispose();
                    }
                    if (Command != null)
                    {
                        if (Command.Connection.State != ConnectionState.Closed)
                        {
                            Command.Connection.Close();
                        }
                        Command.Dispose();
                    }
                }
            }
            if (ex != null)
                throw ex;
            else
                return response;
        }

        /// <summary>
        /// Obtiene la columna con invocación al tipo solicitado y si no lo encuentra devuelve el default del tipo T solicitado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public T GetColumn<T>(string columnName)
        {
            if (Reader[columnName] != DBNull.Value)
            {
                Type typeT = typeof(T);
                if (typeT.IsGenericType && typeT.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    var underlyingType = Nullable.GetUnderlyingType(typeT);
                    var val = Convert.ChangeType(Reader[columnName], underlyingType);
                    return (T)val;
                }
                else
                {
                    return (T)Convert.ChangeType(Reader[columnName], typeof(T));
                }
            }
            else
            {
                return default(T);
            }
        }

        public List<T> GetMappedList<T>() where T : class, new()
        {
            List<T> result = new List<T>();
            var properties = typeof(T).GetProperties();
            while (Read())
            {
                T obj = new T();
                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(Entities.Common.DbColumnAttribute));
                    if (attribute != null)
                    {
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(property.Name);
                        if (propertyInfo != null && propertyInfo.CanWrite)
                        {
                            var columnName = (Entities.Common.DbColumnAttribute)attribute;
                            propertyInfo.SetValue(obj, Reader[columnName.Name] == DBNull.Value ? null : Reader[columnName.Name], null);
                            property.GetType();
                        }
                    }
                }
                result.Add(obj);
            }

            return result;
        }

        #endregion

        #region Liberación de Recursos

        public void Dispose()
        {
            if (sqlRead != null)
            {
                sqlRead.Dispose();
            }
            if (Command != null)
            {
                Command.Dispose();
            }
            if (sqlTransaction != null)
            {
                sqlTransaction.Dispose();
            }
            if (Connection != null)
            {
                Connection.Dispose();
            }
        }

        #endregion

    }
}
