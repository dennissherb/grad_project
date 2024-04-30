using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datalayer.query_classes
{
    public class AccountQuery : BaseQuery
    {
        string tableName = "accounts";
        public static async Task<bool> TryLogin(Dictionary<string, string> user)
        {
            try
            {
                string sanitizedField1 = SanitizeInput(user["accounts_email"]);
                string sanitizedField2 = SanitizeInput(user["accounts_password"]);

                string query = $@"SELECT accounts_id FROM my_project.accounts WHERE (accounts_email = '{sanitizedField1}' OR accounts_user_name = '{sanitizedField1}') AND accounts_password = '{sanitizedField2}'";

                List<Dictionary<string, string>> result = await DBConnection.ExecuteQuery(query);

                return result.Count != 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors appropriately
                Console.WriteLine($"Error during login attempt: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> CreateAccount(Dictionary<string, string> user)
        {
            return await CreateEntry("accounts", user);
        }

        public static async Task<Dictionary<string, string>> ReadAccountByEmail(string email)
        {
            return await ReadEntryByColumn("accounts", "accounts_email", email);
        }
        public static async Task<Dictionary<string, string>> ReadAccountByIdAsync(Dictionary<string, string> user)
        {
            if (user.TryGetValue("accounts_id", out string id) && !string.IsNullOrEmpty(id))
            {
                return await ReadEntryById("accounts", id);
            }
            else
            {
                return null;
            }
        }
        public static async Task<Dictionary<string, string>> ReadAccountById(int id)
        {
            return await ReadEntryById("accounts", id.ToString());
        }

        public static async Task<Dictionary<string, string>> ReadAccountByUQ(Dictionary<string, string> user)
        {
            return await ReadEntryByUniqueQuery("accounts", new Dictionary<string, string> {
                { "accounts_email", user.ContainsKey("accounts_email") ? user["accounts_email"] : "" },
                { "accounts_user_name", user["accounts_email"] }
            });
        }
        public static async Task<List<Dictionary<string, string>>> ReadAccounts()
        {
            return await ReadEntries("accounts");
        }

        public static async Task<Dictionary<string, string>> UpdateAccount(Dictionary<string, string> newUser)
        {
            return await UpdateEntry(newUser, "accounts");
        }

        public static async Task<bool> DeleteAccount(Dictionary<string, string> user)
        {
            Dictionary<string, object> objDict = new Dictionary<string, object>();
            foreach (var kvp in user)
            {
                objDict.Add(kvp.Key, kvp.Value);
            }
            return await DeleteRow("accounts", "email", objDict);
        }
    }
}
