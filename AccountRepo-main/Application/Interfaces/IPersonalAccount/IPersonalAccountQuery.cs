using Domain.Models;

namespace Application.Interfaces.IPersonalAccount
{
    public interface IPersonalAccountQuery
    {
        Task<List<PersonalAccount>> GetAllPersonalAccounts();
        Task<PersonalAccount> GetPersonalAccount(Guid id);
    }
}
