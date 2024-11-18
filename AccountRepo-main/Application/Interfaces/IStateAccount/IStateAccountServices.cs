using Application.Response;
using Domain.Models;

namespace Application.Interfaces.IStateAccount
{
    public interface IStateAccountServices
    {
        Task<List<GenericResponse>> GetAllStateAccounts();
        Task<StateAccount> GetById(int id);
    }
}
