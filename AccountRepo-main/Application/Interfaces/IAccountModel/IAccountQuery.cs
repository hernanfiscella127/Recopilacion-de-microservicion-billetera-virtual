using Domain.Models;

namespace Application.Interfaces.IAccountModel
{
    public interface IAccountQuery
    {
        Task<AccountModel> GetAccountById(Guid accountId);
        Task<AccountModel> GetAccountByAliasOrCBU(string searchParam);
        Task<bool> IsAccountNumberUnique(string accountNumber);
        Task<bool> IsCbuUnique(string cbu);
        Task<bool> IsAliasUnique(string alias);
        Task<bool> UserExists(int userId);
        Task<AccountModel> GetAccountByUser(int userId);
    }
}
