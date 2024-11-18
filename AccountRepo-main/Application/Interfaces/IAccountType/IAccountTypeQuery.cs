using Domain.Models;

namespace Application.Interfaces.IAccountType
{
    public interface IAccountTypeQuery
    {
        Task<List<AccountType>> GetAllAccountTypes();
        Task<AccountType> GetAccountType(int id);
    }
}
