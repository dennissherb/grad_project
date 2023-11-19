using MySql.Data.MySqlClient;

namespace Database
{
    public class DBConnectionBase
    {

        public async Task<void> ExecuteQuery()
        {
            using var connection = new MySqlConnection(ConnectionString);

            await connection.OpenAsync();

            using var command = new MySqlCommand("SELECT field FROM table;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var value = reader.GetValue(0);
                // do something with 'value'
            }
        }
    }
}