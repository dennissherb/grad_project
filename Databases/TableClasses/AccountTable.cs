using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using ZstdSharp.Unsafe;

namespace Databases.TableClasses
{
    public class AccountTable
    {
        List<Account> account;

        public AccountTable(List<string> acc_emails, List<string> acc_usr_names, List<string> acc_dobs)
        {
            account = new List<Account>();
        }

        public void AccountTableFill(string query) { }
    }
}