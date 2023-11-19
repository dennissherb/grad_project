using Database;
namespace Testing
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<string> gmail_users = DBConnection.ExecuteQuery("SELECT * FROM table ").Result;
        }
    }
}