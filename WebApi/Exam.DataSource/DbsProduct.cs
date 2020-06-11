using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataSource
{
    public class DbsProduct
    {

        public List<Exam.Entities.Product> Retrive()
        {

            var command = new SqlCommand("uspProducts", DbsConnections.SqlBCApp) { CommandType = CommandType.StoredProcedure };

            var result = DbsSqlOperations.GetDataSet(command).Tables[0].AsEnumerable()
                .Select(dataRow => new Exam.Entities.Product
                {
                    IdProduct = dataRow.Field<int>("IdProduct"),
                    Product1 = dataRow.Field<string>("Product"),
                    Cost = dataRow.Field<double>("Cost"),
                    Inventary = dataRow.Field<int>("Inventary")
                }).ToList();



            return result;

        }
    }
}
