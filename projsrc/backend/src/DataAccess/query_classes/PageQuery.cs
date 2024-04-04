using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Datalayer;

namespace Datalayer.Queries
{
    public class PageQuery : BaseQuery
    {

        public static async Task<bool> CreatePage(Dictionary<string, string> product)
        {
            return await CreateEntry("pages", product);
        }

        public static async Task<Dictionary<string, string>> ReadPageById(int id)
        {
            return await ReadEntryById("pages", id.ToString());
        }

        public static async Task<List<Dictionary<string, string>>> ReadPages()
        {
            return await ReadEntries("pages");
        }

        public static async Task<bool> UpdatePageAsync(BasePage Page)
        {
            Dictionary<string, object> d = new();
            d.Add("pages_id", Page.id);
            d.Add("pages_type", Page.pageType);

            string query = $@"UPDATE 'pages' 
                                SET pages_type = '{d["pages_type"]}'
                                WHERE pages_id = '{d["pages_id"]}';";

            return (await DBConnection.ExecuteNonQuery(query) > 0);
        }
        public static async Task<bool> UpdateContent(BasePage Page)
        {
            Dictionary<string, object> d = new();
            d.Add("pages_id", Page.id);
            d.Add("pages_type", Page.pageType);

            string query = $@"UPDATE 'pages' 
                                SET pages_type = '{d["pages_type"]}'
                                WHERE pages_id = '{d["pages_id"]}';";

            return (await DBConnection.ExecuteNonQuery(query) > 0);
        }
        public static async Task<bool> DeletePage(int id)
        {
            return await DeleteRowById("pages", id);
        }
    }
}