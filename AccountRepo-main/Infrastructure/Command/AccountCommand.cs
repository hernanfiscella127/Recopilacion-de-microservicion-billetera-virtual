using Account.API.Infrastructure;
using Application.Interfaces.IAccountModel;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Command
{
    public class AccountCommand : IAccountCommand
    {
        private readonly AccountContext _context;

        public AccountCommand(AccountContext context)
        {
            _context = context;
        }
        public async Task InsertAccount(AccountModel account)
        {
            await _context.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccount(AccountModel account)
        {
            _context.Account.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBalance(Guid accountid, decimal balance)
        {
            var account = await _context.Account
                .FirstOrDefaultAsync(p => p.AccountId == accountid);
            account.Balance += balance;
            _context.Update(account);
            await _context.SaveChangesAsync();
        } 
    }
}
