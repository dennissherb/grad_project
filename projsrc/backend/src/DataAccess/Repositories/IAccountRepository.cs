using DataObjects;

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
        Task<Account> GetAccountByUserNameAsync(string email);
    }
}