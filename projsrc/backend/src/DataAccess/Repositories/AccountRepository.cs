using Datalayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datalayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyProjectContext _ctx;

        public AccountRepository(MyProjectContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _ctx.Accounts.ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _ctx.Accounts.FindAsync(id);
        }

        public async Task CreateAccountAsync(Account account)
        {
            _ctx.Accounts.Add(account);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _ctx.Accounts.Update(account);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _ctx.Accounts.FindAsync(id);
            if (account != null)
            {
                _ctx.Accounts.Remove(account);
                await _ctx.SaveChangesAsync();
            }
        }
        public async Task<Account> GetAccountByEmailAsync(string email)
        {
            return await _ctx.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
