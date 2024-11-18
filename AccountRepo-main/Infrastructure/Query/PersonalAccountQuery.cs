using Account.API.Infrastructure;
using Application.Interfaces.IPersonalAccount;
using Domain.Models;

namespace AccountInfrastructure.Query
{
    public class PersonalAccountQuery : IPersonalAccountQuery
    {
        private readonly AccountContext _context;

        public PersonalAccountQuery(AccountContext context)
        {
            _context = context;
        }
        public Task<List<PersonalAccount>> GetAllPersonalAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<PersonalAccount> GetPersonalAccount(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
