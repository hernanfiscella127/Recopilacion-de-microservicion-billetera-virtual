using Domain.Models;

namespace Application.Interfaces.IAccountModel
{
    public interface IAccountCommand
    {
        Task InsertAccount(AccountModel account);
        Task UpdateAccount(AccountModel account);
        Task UpdateBalance(Guid accountid, decimal balance);
    }
}
