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
        public static async Task<Dictionary<string, string>> ReadAccountByIdAsync(Dictionary<string,string> user)
        {
            try
            {
                string id = string.Empty;
                user.TryGetValue("accounts_id", out id);

                if (id == null || id == string.Empty)
                    return null;

                string query = $@"SELECT * FROM my_project.accounts WHERE accounts_id = '{user["accounts_id"]}'";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result != null && result.Count > 0 ? result[0] : null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error reading account by id: {ex.Message}");
                return null;
            }
        }
        public static async Task<Dictionary<string, string>> ReadAccountById(int id)
        {
            try
            {

                string query = $@"SELECT * FROM my_project.accounts WHERE accounts_id = '{id}'";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result != null && result.Count > 0 ? result[0] : null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error reading account by id: {ex.Message}");
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
        public static async Task<Dictionary<string,string>> UpdateAccount(Dictionary<string,string> oldUser, Dictionary<string,string> newUser)
        {
            oldUser = await AccountQuery.ReadAccountByUQ(oldUser);
            Dictionary<string, string> mergedDictionary = oldUser
            .Concat(newUser)
            .GroupBy(kv => kv.Key)
            .ToDictionary(g => g.Key, g => g.Last().Value);
            try
            {

                string query = $@"UPDATE my_project.accounts
                                SET accounts_email = {mergedDictionary["accounts_email"]},
                                    accounts_user_name = {mergedDictionary["accounts_user_name"]},
                                    accounts_date_of_birth = {mergedDictionary["accounts_date_of_birth"]},
                                    accounts_password = {mergedDictionary["accounts_password"]}
                                WHERE accounts_email = {mergedDictionary["accounts_email"]} OR accounts_user_name = {mergedDictionary["accounts_user_name"]} OR accounts_id = {mergedDictionary["accounts_id"]}";
                int result = await DBConnection.ExecuteNonQuery(query);
                if (result > 0)
                    return mergedDictionary;
                else
                    return null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error updating account: {ex.Message}");
                return null;
            }
        }
        public static async Task<Dictionary<string,string>> UpdateAccount(Dictionary<string,string> newUser)
        {
            Dictionary<string, string> oldUser = await AccountQuery.ReadAccountByIdAsync(newUser);
            //Dictionary<string, string> mergedDictionary = oldUser
            //.Concat(newUser)
            //.GroupBy(kv => kv.Key)
            //.ToDictionary(g => g.Key, g => g.Last().Value);

            Dictionary<string, string> mergedDictionary = new(oldUser);
            foreach (KeyValuePair<string,string> pair in newUser)
            {
                if (pair.Value != "")
                    mergedDictionary[pair.Key] = pair.Value;
            }


            try
            {

                string query = $@"UPDATE my_project.accounts
                                SET accounts_email = '{mergedDictionary["accounts_email"]}',
                                    accounts_user_name = '{mergedDictionary["accounts_user_name"]}',
                                    accounts_date_of_birth = '{mergedDictionary["accounts_date_of_birth"]}',
                                    accounts_password = '{mergedDictionary["accounts_password"]}'
                                WHERE accounts_id = '{mergedDictionary["accounts_id"]}'";
                int result = await DBConnection.ExecuteNonQuery(query);
                if (result > 0)
                    return mergedDictionary;
                else
                    return null;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error updating account: {ex.Message}");
                return null;
            }
        }

        //delete an account entry by referring to it with UQ without password
        public static async Task<bool> DeleteAccountAsAdminByUQ(Dictionary<string,string> user)
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

        //delete an account entry by referring to tit with UQ and password
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
