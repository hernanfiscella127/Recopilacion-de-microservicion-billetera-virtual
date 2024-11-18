using Account.API.Infrastructure;
using Application.Interfaces.IAccountModel;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountInfrastructure.Query
{
    public class AccountQuery : IAccountQuery
    {
        private readonly AccountContext _context;

        public AccountQuery(AccountContext context)
        {
            _context = context;
        }
        public Task<AccountModel> GetAccountById(Guid accountId)
        {
            var account = _context.Account
                .FirstOrDefaultAsync(x => x.AccountId == accountId);
            return account;
        }

        public async Task<bool> IsAccountNumberUnique(string accountNumber)
        {
            return !await _context.Account
                .AnyAsync(a => a.NumberAccount == accountNumber);
        }

        public async Task<bool> IsCbuUnique(string cbu)
        {
            return !await _context.Account
                .AnyAsync(a => a.CBU == cbu);
        }

        public async Task<bool> IsAliasUnique(string alias)
        {
            return !await _context.Account
                .AnyAsync(a => a.Alias == alias);
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _context.Account
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<AccountModel> GetAccountByUser(int userId)
        {
            var account = await _context.Account
                .FirstOrDefaultAsync(x => x.UserId == userId);
            return account;
        }
        public async Task<AccountModel> GetAccountByAliasOrCBU(string searchParam)
        {
            var account = await _context.Account
                .FirstOrDefaultAsync(x => x.Alias == searchParam || x.CBU == searchParam);
            return account;
        }
    }
}
