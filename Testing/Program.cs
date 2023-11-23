using Database;
using Databases.TableClasses;

namespace Testing
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<AccountTable> gmail_users = DBConnection.ExecuteQuery("SELECT accounts_email FROM my_project.accounts \r\n").Result;
            Console.WriteLine(gmail_users.Count); ;
            Console.Read();
        }

    }
}