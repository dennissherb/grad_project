using Microsoft.Extensions.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace Database
{
    public class DBConnection
    {
        private DBConnection()
        {
            //Create json reader
            string fileName = "appsettings.json";
            string jsonString = File.ReadAllText(fileName);
            DBConnection connection = JsonSerializer.Deserialize<DBConnection>(jsonString)!;


            //Get values from json file
            this.Server = connection.Server;
            this.DatabaseName = connection.DatabaseName;
            this.User = connection.User;
            this.Password = connection.Password;

            //Create the connection string
            this.ConnectionString = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", this.Server, this.DatabaseName, this.User, this.Password);
        }

        public string Server { get { return Server; }  set { Server = value; } }
        public string DatabaseName { get { return DatabaseName; } set { DatabaseName = value; } }
        public string User { get { return User; } set { User = value; } }
        public string Password { get { return Password; } set { Password = value; } }
        public string ConnectionString { get { return ConnectionString; } set { ConnectionString = value; } }


        public static async Task<List<Type>> ExecuteQuery(string query)
        {
            DBConnection connectionObj = new DBConnection();
            List<Type> result = new List<Type>();
            using var connection = new MySqlConnection(connectionObj.ConnectionString);

            await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var value = reader.GetValue(0);
                Console.WriteLine(reader.GetValue(0));
            }
            return result;
        }
    }
}
