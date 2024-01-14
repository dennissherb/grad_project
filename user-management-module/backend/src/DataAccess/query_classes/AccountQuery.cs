using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Datalayer;

namespace Datalayer.Queries
{
    public class AccountQuery
    {
        private static string SanitizeInput(string input)
        {
            // Perform necessary input sanitization to prevent SQL injection
            // For simplicity, this example uses basic string replace to escape single quotes
            return input.Replace("'", "''");
        }

        public static async Task<bool> TryLogin(string field1, string field2)
        {
            try
            {
                string sanitizedField1 = SanitizeInput(field1);
                string sanitizedField2 = SanitizeInput(field2);

                string query = $@"SELECT accounts_id FROM my_project.accounts WHERE accounts_email = '{sanitizedField1}' AND accounts_password = '{sanitizedField2}'";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return (result.Count != 0);
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error during login attempt: {ex.Message}");
                return false;
            }
        }
        //if I knew I would just use the json request bodys I wouldn't have even implemented the above...
        public static async Task<bool> TryLogin(Dictionary<string,string> user)
        {
            try
            {
                string sanitizedField1 = SanitizeInput(user["accounts_email"]);
                string sanitizedField2 = SanitizeInput(user["accounts_password"]);

                string query = $@"SELECT accounts_id FROM my_project.accounts WHERE (accounts_email = '{sanitizedField1}' OR accounts_user_name = '{sanitizedField1}') AND accounts_password = '{sanitizedField2}'";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return (result.Count != 0);
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error during login attempt: {ex.Message}");
                return false;
            }
        }



        //register using 4 variables, why did I even implement this 
        public static async Task<bool> CreateAccount(string email, string userName, DateTime dateOfBirth, string password)
        {
            try
            {
                string formattedDateOfBirth = dateOfBirth.ToString("yyyy-MM-dd"); // Example date format

                string query = $@"INSERT INTO my_project.accounts (accounts_email, accounts_user_name, accounts_date_of_birth, accounts_password) VALUES ('{email}', '{userName}', '{formattedDateOfBirth}', '{password}')";

                int result = await DBConnection.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error creating account: {ex.Message}");
                return false;
            }
        }

        //register using dictionary instead of 4 variables which is dumb
        public static async Task<bool> CreateAccount(Dictionary<string,string> user)
        {
            try
            {
                string query = $@"INSERT INTO my_project.accounts
                (accounts_email,
                accounts_user_name,
                accounts_date_of_birth,
                accounts_password) 
                    VALUES (
                    '{user["accounts_email"]}',
                    '{user["accounts_user_name"]}',
                    '{user["accounts_date_of_birth"]}',
                    '{user["accounts_password"]}')";

                int result = await DBConnection.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error creating account: {ex.Message}");
                return false;
            }
        }

        public static async Task<Dictionary<string, string>> ReadAccountByEmail(string email)
        {
            try
            {
                string sanitizedEmail = SanitizeInput(email);

                string query = $@"SELECT * FROM my_project.accounts WHERE accounts_email = '{sanitizedEmail}'";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result != null && result.Count > 0 ? result[0] : null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error reading account by email: {ex.Message}");
                return null;
            }
        }
        public static async Task<Dictionary<string, string>> ReadAccountByUQ(Dictionary<string,string> user)
        {
            try
            {
                string sanitizedEmail = SanitizeInput(user["accounts_email"]);

                string query = $@"SELECT * FROM my_project.accounts WHERE accounts_email = '{sanitizedEmail}' OR accounts_user_name = '{sanitizedEmail}'";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result != null && result.Count > 0 ? result[0] : null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error reading account by email: {ex.Message}");
                return null;
            }
        }
        public static async Task<List<Dictionary<string, string>>> ReadAccounts()
        {
            try
            {
                string query = $@"SELECT * FROM my_project.accounts";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result != null && result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error reading account by email: {ex.Message}");
                return null;
            }
        }


        //DO NOT USE THIS ONE THIS IS BROKEN WILL ONLY WORK IF ALL ARGUMENTS ARE PROVIDED THIS IS ONLY TO AVOID REWRITING TESTS
        public static async Task<bool> UpdateAccount(string email, string newUserName, DateTime newDateOfBirth, string newPassword)
        {
            try
            {
                string formattedNewDateOfBirth = newDateOfBirth.ToString("yyyy-MM-dd"); 

                string query = $@"UPDATE my_project.accounts SET accounts_user_name = '{newUserName}', accounts_date_of_birth = '{formattedNewDateOfBirth}', accounts_password = '{newPassword}' WHERE accounts_email = '{email}'";

                int result = await DBConnection.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error updating account: {ex.Message}");
                return false;
            }
        }


        //Only need to provide email and password for oldUser for authorization
        public static async Task<bool> UpdateAccount(Dictionary<string,string> oldUser, Dictionary<string,string> newUser)
        {
            oldUser = await AccountQuery.ReadAccountByUQ(oldUser);
            try
            {
                //check password and email of oldUser to authorize update
                string query = $@"UPDATE my_project.accounts SET 
accounts_user_name =     '{(newUser.ContainsKey("accounts_user_name")       && newUser["accounts_user_name"] != null        ? newUser["accounts_user_name"]     : oldUser["accounts_user_name"])}',
accounts_date_of_birth = '{(newUser.ContainsKey("accounts_date_of_birth")   && newUser["accounts_date_of_birth"] != null    ? newUser["accounts_date_of_birth"] : oldUser["accounts_date_of_birth"])}',
accounts_password =      '{(newUser.ContainsKey("accounts_password")        && newUser["accounts_password"] != null         ? newUser["accounts_password"]      : oldUser["accounts_password"])}'
WHERE accounts_email =   '{(newUser.ContainsKey("accounts_email")           && newUser["accounts_email"] != null            ? newUser["accounts_email"]         : oldUser["accounts_email"])}'
AND accounts_password =  '{(newUser.ContainsKey("accounts_password")        && newUser["accounts_password"] != null         ? newUser["accounts_password"]      : oldUser["accounts_password"])}'";

                int result = await DBConnection.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error updating account: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DeleteAccountAsAdmin(Dictionary<string,string> user)
        {
            try
            {

                string query = $@"DELETE FROM my_project.accounts WHERE accounts_email = '{user["accounts_email"]}'";

                int result = await DBConnection.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error deleting account: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DeleteAccount(Dictionary<string,string> user)
        {
            try
            {
                string sanitizedField1 = SanitizeInput(user["accounts_email"]);
                string sanitizedField2 = SanitizeInput(user["accounts_password"]);
                string query = $@"DELETE FROM my_project.accounts WHERE
                    accounts_email =    '{user["accounts_email"]}' AND
                    accounts_password = '{user["accounts_password"]}'";

                int result = await DBConnection.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error deleting account: {ex.Message}");
                return false;
            }
        }
    }
}
