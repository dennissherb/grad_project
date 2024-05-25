using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Datalayer
{
    public class DBConnection
    {
        private readonly IConfiguration _configuration;

        public DBConnection()
        {
            string filepath = Path.GetFullPath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName, "config", "mysql-config.json"));
            if (File.Exists("filepath"))
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .AddJsonFile(filepath)
                    .Build();
                _configuration = configuration.GetSection("DBConnection");
            }
            else
            {
                _configuration = null;
            }
        }
        public string ConnectionString { get; set; }
        private string GetConnectionString()
        {
            string server;
            string databaseName;
            string user;
            string password;

            if (_configuration != null)
            {
                server = _configuration["Server"];
                databaseName = _configuration["DatabaseName"];
                user = _configuration["User"];
                password = _configuration["Password"];
            }
            else
            {
                server = "localhost";
                databaseName = "my_project";
                user = "root";
                password = "josh17rog";
            }

            return $"Server={server};Database={databaseName};Uid={user};Pwd={password};";
        }
        public static async Task<List<Dictionary<string, string>>> ExecuteQuery(string query)
        {
            DBConnection connectionObj = new DBConnection();
            using var connection = new MySqlConnection(connectionObj.GetConnectionString());
            await connection.OpenAsync();
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            List<Dictionary<string, string>> queryResult = new List<Dictionary<string, string>>();
            while (await reader.ReadAsync())
            {
                Dictionary<string, string> row = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());
                }
                queryResult.Add(row);
            }
            await connection.CloseAsync();
            return queryResult;
        }
        public static async Task<int> ExecuteNonQuery(string query)
        {
            DBConnection connectionObj = new DBConnection();
            using var connection = new MySqlConnection(connectionObj.GetConnectionString());
            await connection.OpenAsync();
            using var command = new MySqlCommand(query, connection);
            using System.Data.Common.DbDataReader reader = await command.ExecuteReaderAsync();
            int RecordsAffected = reader.RecordsAffected;
            await connection.CloseAsync();
            return RecordsAffected;
        }
    }
}
