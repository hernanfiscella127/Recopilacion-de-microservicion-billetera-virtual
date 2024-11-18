using Account.API.Infrastructure;
using Application.Interfaces.IStateAccount;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountInfrastructure.Query
{
    public class StateAccountQuery : IStateAccountQuery
    {
        private readonly AccountContext _context;

        public StateAccountQuery(AccountContext context)
        {
            _context = context;
        }

        public async Task<StateAccount> GetStateAccount(int id)
        {
            var stateAccount = await _context.StateAccount
                .FirstOrDefaultAsync(x => x.Id == id);

            return stateAccount;
        }

        public async Task<List<StateAccount>> GetAllStateAccounts()
        {
            var results = await _context.StateAccount.ToListAsync();
            return results;
        }


    }
}
