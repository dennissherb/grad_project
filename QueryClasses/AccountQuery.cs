using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Databases;

namespace QueryClasses
{
    public class AccountQuery
    {
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

        private static string SanitizeInput(string input)
        {
            // Perform necessary input sanitization to prevent SQL injection
            // For simplicity, this example uses basic string replace to escape single quotes
            return input.Replace("'", "''");
        }

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

        public static async Task<bool> UpdateAccount(string email, string newUserName, DateTime newDateOfBirth, string newPassword)
        {
            try
            {
                string formattedNewDateOfBirth = newDateOfBirth.ToString("yyyy-MM-dd"); // Example date format

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

        public static async Task<bool> DeleteAccount(string email)
        {
            try
            {
                string sanitizedEmail = SanitizeInput(email);

                string query = $@"DELETE FROM my_project.accounts WHERE accounts_email = '{sanitizedEmail}'";

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
