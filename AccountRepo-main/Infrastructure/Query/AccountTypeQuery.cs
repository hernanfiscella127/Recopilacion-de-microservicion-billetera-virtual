using Account.API.Infrastructure;
using Application.Interfaces.IAccountType;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountInfrastructure.Query
{
    public class AccountTypeQuery : IAccountTypeQuery
    {
        private readonly AccountContext _context;

        public AccountTypeQuery(AccountContext context)
        {
            _context = context;
        }
        public async Task<AccountType> GetAccountType(int id)
        {
            var accountType = await _context.AccountType
                .FirstOrDefaultAsync(x => x.Id == id);

            return accountType;
        }

        public async Task<List<AccountType>> GetAllAccountTypes()
        {
            var results = await _context.AccountType.ToListAsync();
            return results;
        }
    }
}
