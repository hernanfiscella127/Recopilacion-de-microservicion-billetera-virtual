using Application.Request;
using Application.Response;

namespace Application.Interfaces.IAccountModel
{
    public interface IAccountServices
    {
        Task<AccountResponse> CreateAccount(int userId, AccountCreateRequest accountRequest);
        Task<AccountResponse> GetById(Guid id);
        Task<AccountResponse> GetByAliasOrCBU(string searchParam);
        Task<AccountResponse> GetByUserId(int userId);
        Task<AccountResponse> UpdateAccount(Guid id, AccountUpdateRequest accountRequest);
        Task<TransferProcess> UpdateBalance(Guid id, AccountBalanceRequest balance);
        Task<AccountResponse> DisableAccountByUser(int UserId);
    }
}