using Microsoft.Extensions.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace Databases
{
    public class DBConnection
    {
        private readonly IConfiguration _configuration;

        public DBConnection()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _configuration = configuration.GetSection("DBConnection");
        }
        public string ConnectionString { get; set; }
        private string GetConnectionString()
        {
            string server = _configuration["Server"];
            string databaseName = _configuration["DatabaseName"];
            string user = _configuration["User"];
            string password = _configuration["Password"];

            return $"Server={server};Database={databaseName};Uid={user};Pwd={password};";
        }

        public static async Task<List<Dictionary<string,string>>> ExecuteQuery(string query)
        {
            DBConnection connectionObj = new DBConnection();
            using var connection = new MySqlConnection(connectionObj.GetConnectionString());
            await connection.OpenAsync();
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            List<Dictionary<string,string>> queryResult = new List<Dictionary<string,string>>();
            while (await reader.ReadAsync())
            {
                Dictionary<string,string> row = new Dictionary<string, string>(); 
                for (int i = 0; i < reader.FieldCount; i++) 
                {
                    row.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());
                }
                queryResult.Add(row);
            }
            await connection.CloseAsync();
            return queryResult;
        }
    }
}
