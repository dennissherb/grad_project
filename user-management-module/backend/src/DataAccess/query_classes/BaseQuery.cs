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
                Console.WriteLine($"Error deleting: {ex.Message}");
                return false;
            }
        }

        protected static async Task<bool> CreateEntry(string tableName, Dictionary<string, string> data)
        {
            try
            {
                string columns = string.Join(",", data.Keys);
                string values = string.Join(",", data.Values);

                string query = $"INSERT INTO my_project.{tableName} ({columns}) VALUES ({values})";

                int result = await DBConnection.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error creating entry in {tableName}: {ex.Message}");
                return false;
            }
        }
        
        protected static async Task<Dictionary<string, string>> ReadEntryByColumn(string tableName, string columnName, string columnValue)
        {
            try
            {
                string sanitizedColumnValue = SanitizeInput(columnValue);

                string query = $"SELECT * FROM my_project.{tableName} WHERE {columnName} = '{sanitizedColumnValue}'";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result != null && result.Count > 0 ? result[0] : null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error reading entry by {columnName}: {ex.Message}");
                return null;
            }
        }

        protected static async Task<Dictionary<string, string>> ReadEntryById(string tableName, string idValue)
        {
            try
            {
                string sanitizedIdValue = SanitizeInput(idValue);

                string idColumnName = tableName + "_id";

                string query = $"SELECT * FROM my_project.{tableName} WHERE {idColumnName} = '{sanitizedIdValue}'";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result != null && result.Count > 0 ? result[0] : null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error reading entry by ID: {ex.Message}");
                return null;
            }
        }

        protected static async Task<Dictionary<string, string>> ReadEntryByUniqueQuery(string tableName, Dictionary<string, string> uniqueColumns)
        {
            try
            {
                List<string> conditions = new List<string>();
                foreach (var pair in uniqueColumns)
                {
                    string sanitizedValue = SanitizeInput(pair.Value);
                    conditions.Add($"{pair.Key} = '{sanitizedValue}'");
                }

                string joinedConditions = string.Join(" OR ", conditions);

                string query = $"SELECT * FROM my_project.{tableName} WHERE {joinedConditions}";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result != null && result.Count > 0 ? result[0] : null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error reading entry by unique query: {ex.Message}");
                return null;
            }
        }
        protected static async Task<List<Dictionary<string, string>>> ReadEntries(string tableName)
        {
            try
            {
                string query = $"SELECT * FROM my_project.{tableName}";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error reading entries from {tableName}: {ex.Message}");
                return null;
            }
        }
        protected static async Task<Dictionary<string, string>> UpdateEntry(Dictionary<string, string> newUser, string tableName)
        {
            try
            {
                Dictionary<string, string> oldUser = await ReadEntryById(tableName, newUser[$"{tableName}_id"]);
                if (oldUser == null)
                    return null;

                Dictionary<string, string> mergedDictionary = new Dictionary<string, string>(oldUser);
                foreach (KeyValuePair<string, string> pair in newUser)
                {
                    if (!string.IsNullOrEmpty(pair.Value))
                        mergedDictionary[pair.Key] = pair.Value;
                }

                List<string> updates = new List<string>();
                foreach (var pair in mergedDictionary)
                {
                    updates.Add($"{pair.Key} = '{pair.Value}'");
                }

                string joinedUpdates = string.Join(", ", updates);

                string query = $"UPDATE my_project.{tableName} SET {joinedUpdates} WHERE accounts_id = '{mergedDictionary["accounts_id"]}'";
                int result = await DBConnection.ExecuteNonQuery(query);

                return result > 0 ? mergedDictionary : null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error updating entry in {tableName}: {ex.Message}");
                return null;
            }
        }
    }
}
