using System.Configuration;
using Databases;
using Databases.TableClasses;

namespace Testing
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Dictionary<string,string>> gmail_users = DBConnection.ExecuteQuery(@"SELECT accounts.* FROM my_project.accounts 
                 WHERE accounts_email LIKE '%gmail%'").Result;
            System.Console.WriteLine(gmail_users[0]["accounts_email"]);
        }

    }
}