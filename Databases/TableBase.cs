using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    public abstract class TableBase
    {
        protected readonly string ConnectionString;
        protected readonly string TableName;

        protected TableBase(string connectionString, string tableName)
        {
            ConnectionString = connectionString;
            TableName = tableName;
        }

        protected IEnumerable<dynamic> Query(string sql, object parameters = null)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query(sql, parameters);
            }
        }

        protected dynamic QueryFirstOrDefault(string sql, object parameters = null)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault(sql, parameters);
            }
        }

        protected int Execute(string sql, object parameters = null)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Execute(sql, parameters);
            }
        }

        // Add more common methods as needed
    }
}
