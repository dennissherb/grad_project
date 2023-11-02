using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Databases
{
    public class Account_table : DB
    {
        List<string> acc_emails;
        List<string> acc_usr_names;
        List<string> acc_dobs;


        public Account_table(List<string> acc_emails, List<string> acc_usr_names, List<string> acc_dobs)
        {
            this.acc_emails = acc_emails;
            this.acc_usr_names = acc_usr_names;
            this.acc_dobs = acc_dobs;

            config.ConnectionString = base.settings.GetConnectionString("MySqlConnection");
        }


    }
}