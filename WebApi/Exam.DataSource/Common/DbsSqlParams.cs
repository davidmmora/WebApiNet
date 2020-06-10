using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataSource
{
    class DbsSqlParams
    {
        /// <summary>
        /// Obtiene un parámetro SqlServer inicializado con los parámetros proporcionados
        /// </summary>
        /// <param name="Name">Nombre del parámetro Sql</param>
        /// <param name="dbType">Tipo de dato</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns></returns>
        public static SqlParameter Direct(string Name, System.Data.SqlDbType dbType, object value)
        {
            SqlParameter sPar = new SqlParameter(Name, dbType);
            sPar.Value = value == null ? DBNull.Value : value;
            return sPar;
        }

        /// <summary>
        /// Crea un parámetro no inicializado de salida
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="dbType">Tipo de dato en Base Sql Server</param>
        /// <returns></returns>
        public static SqlParameter Output(string name, SqlDbType dbType)
        {
            SqlParameter sPar = new SqlParameter(name, dbType);
            sPar.Direction = ParameterDirection.Output;

            return sPar;
        }

        /// <summary>
        /// Obtiene un parámetro SqlServer inicializado con los parámetros proporcionados,
        /// si el valor del parámetro es el valor mínimo de clase para algunos casos asignará
        /// valor nulo al parámetro.
        /// </summary>
        /// <param name="Name">Nombre del parámetro Sql</param>
        /// <param name="dbType">Tipo de dato</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns></returns>
        public static SqlParameter Optional(string Name, SqlDbType dbType, object value, int? lenght = null)
        {
            SqlParameter sPar = new SqlParameter(Name, dbType);
            if (lenght.HasValue && lenght.Value > 0)
            {
                sPar.Size = lenght.Value;
            }
            switch (dbType)
            {
                case SqlDbType.Bit:
                    sPar.Value = value == null ? DBNull.Value : value;
                    break;
                case SqlDbType.DateTime:
                case SqlDbType.Date:
                    sPar.Value = value == null
                                 || Convert.ToDateTime(value) == DateTime.MinValue ? DBNull.Value : value;
                    break;
                case SqlDbType.Decimal:
                case SqlDbType.Float:
                case SqlDbType.Int:
                case SqlDbType.Money:
                case SqlDbType.VarBinary:
                case SqlDbType.SmallInt:
                case SqlDbType.TinyInt:
                case SqlDbType.BigInt:
                    sPar.Value = value == null
                                 || Convert.ToDouble(value) < 0 ? DBNull.Value : value;
                    break;
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                    sPar.Value = string.IsNullOrEmpty((string)value) ? DBNull.Value : value;
                    break;
                case SqlDbType.Char:
                case SqlDbType.Xml:
                    sPar.Value = value == null
                                 || Convert.ToChar(value) == char.MinValue ? DBNull.Value : value;
                    break;
            }
            return sPar;
        }
    }
}
