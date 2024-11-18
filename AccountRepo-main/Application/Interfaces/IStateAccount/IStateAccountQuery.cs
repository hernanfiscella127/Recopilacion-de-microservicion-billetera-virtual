using Domain.Models;

namespace Application.Interfaces.IStateAccount
{
    public interface IStateAccountQuery
    {
        Task<List<StateAccount>> GetAllStateAccounts();
        Task<StateAccount> GetStateAccount(int id);
    }
}
