using Datalayer.Models;

namespace Datalayer.Repositories
{
    public interface IAccountRepository
    {
        Task CreateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);
        Task<Account> GetAccountByIdAsync(int id);
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task UpdateAccountAsync(Account account);
        Task<Account> GetAccountByEmailAsync(string email);
    }
}