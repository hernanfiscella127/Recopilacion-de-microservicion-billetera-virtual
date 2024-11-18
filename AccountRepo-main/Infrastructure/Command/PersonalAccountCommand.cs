using Account.API.Infrastructure;
using Application.Interfaces.IPersonalAccount;
using Domain.Models;

namespace AccountInfrastructure.Command
{
    public class PersonalAccountCommand : IPersonalAccountCommand
    {
        private readonly AccountContext _context;

        public PersonalAccountCommand(AccountContext context)
        {
            _context = context;
        }

        public Task DeletePersonalAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task InsertPersonalAccount(PersonalAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
