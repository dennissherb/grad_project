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
            this.ConnectionString = $"Server={this.Server};Database={this.DatabaseName};Uid={this.User};Pwd={this.Password};"
        }

        public string Server { get { return Server; }  set { Server = value; } }
        public string DatabaseName { get { return DatabaseName; } set { DatabaseName = value; } }
        public string User { get { return User; } set { User = value; } }
        public string Password { get { return Password; } set { Password = value; } }

        //Complete connection string (compound of values)
        public string ConnectionString { get { return ConnectionString; } set { ConnectionString = value; } }

        public MySqlConnection Connection { get; set; }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, User, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}
