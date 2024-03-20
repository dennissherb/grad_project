using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datalayer;

namespace Datalayer.Queries
{
    public class BaseQuery
    {
        public static string SanitizeInput(string input)
        {
            // Perform necessary input sanitization to prevent SQL injection
            // For simplicity, this example uses basic string replace to escape single quotes
            return input.Replace("'", "''");
        }

        public static async Task<bool> DeleteRow(string tableName, string key, Dictionary<string,object> row) 
        {
            try
            {
                string query = $@"DELETE FROM my_project.{tableName} WHERE {tableName}_{key} = '{row[$"{tableName}_{key}"]}'";

                int result = await DBConnection.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error deleting account: {ex.Message}");
                return false;
            }
        }
    }
}
