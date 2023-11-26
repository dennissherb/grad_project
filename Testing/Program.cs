using Databases;
using Databases.TableClasses;

namespace Testing
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Object> gmail_users = DBConnection.ExecuteQuery("SELECT accounts.* FROM my_project.accounts \r\n WHERE accounts_email LIKE '%gmail%'").Result;
            foreach (var item in gmail_users)
            {
                Console.WriteLine(item);
            }

        }

    }
}