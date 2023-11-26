using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{

    class Account
    {
        string accounts_email;
        string accounts_user_name;
        string accounts_date_of_birth;
        string accounts_password;
        
        public bool AddIntoDB() 
        {
           //Task<List<Object>> queryResult = DBConnection.ExecuteQuery(String.Format("INSERT INTO my_project.accounts(accounts_email, accounts_user_name, accounts_date_of_birth, accounts_password)" +
           //    "VALUES({0},{1},{2},{3})", accounts_email, accounts_user_name, accounts_date_of_birth, accounts_password));
            //return queryResult.Result.Count; 
            return true;
        }
    }
}
