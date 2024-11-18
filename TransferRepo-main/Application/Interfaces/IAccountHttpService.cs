using Application.Request;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountHttpService
    {
        Task<AccountResponse> GetAccountById(Guid id);
        Task<AccountResponse> GetAccountByAliasOrCBU(string searchParam);
        Task<TransferProcess> UpdateAccountBalance(Guid id, AccountBalanceRequest balanceData);
        Task<AccountResponse> GetAccountByUserId(Guid userId);
    }
}
