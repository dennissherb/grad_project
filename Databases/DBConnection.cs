using Microsoft.Extensions.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace Databases
{
    public class DBConnection
    {
        private DBConnection()
        {
            //Create json reader
            //string filePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Databases\\appsetting.json");
            //string jsonString = File.ReadAllText(@"C:\Users\CSS\source\repos\grad_project\Databases\appsetting.json");
            //DBConnection connection = JsonSerializer.Deserialize<DBConnection>(jsonString)!;


            //Get values from json file
            //this.Server = connection.Server;
            //this.DatabaseName = connection.DatabaseName;
            //this.User = connection.User;
            //this.Password = connection.Password;

            //Create the connection string
            //this.ConnectionString = String.Format("Server={0};Database={1};Uid={2};Pwd={3};", this.Server, this.DatabaseName, this.User, this.Password);
            this.ConnectionString = "Server=localhost;Database=my_project;Uid=root;Pwd=josh17rog";
        }

        //public string Server { get { return Server; }  set { Server = value; } }
        //public string DatabaseName { get { return DatabaseName; } set { DatabaseName = value; } }
        //public string User { get { return User; } set { User = value; } }
        //public string Password { get { return Password; } set { Password = value; } }
        public string ConnectionString { get; set; }


        public static async Task<List<Dictionary<string,string>>> ExecuteQuery(string query)
        {
            DBConnection connectionObj = new DBConnection();
            using var connection = new MySqlConnection(connectionObj.ConnectionString);
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
