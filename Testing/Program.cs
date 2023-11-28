using System.Configuration;
using Databases;
using QueryClasses;
namespace Testing
{
    class Program
    {
        public static void Main(string[] args)
        {
            // List<Dictionary<string,string>> gmail_users = DBConnection.ExecuteQuery(@"SELECT accounts.* FROM my_project.accounts 
            //      WHERE accounts_email LIKE '%gmail%'").Result;
            // //System.Console.WriteLine(gmail_users[0]["accounts_email"]);
            // for(int i = 0; i < gmail_users.Count ; i++) 
            // {
            //     System.Console.Write(gmail_users[i]["accounts_email"] + " ");
            //     System.Console.Write(gmail_users[i]["accounts_user_name"] + " ");
            //     System.Console.Write(gmail_users[i]["accounts_date_of_birth"] + " ");
            //     System.Console.Write(gmail_users[i]["accounts_password"] + " ");
            //     System.Console.WriteLine("\n");
            // }

            System.Console.WriteLine(AccountQuery.TryLogin("john.doe@gmail.com", "password123").Result);

        }
    }
}