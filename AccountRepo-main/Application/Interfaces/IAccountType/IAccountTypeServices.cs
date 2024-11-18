using Application.Response;
using Domain.Models;

namespace Application.Interfaces.IAccountType
{
    public interface IAccountTypeServices
    {
        Task<List<GenericResponse>> GetAllAccountTypes();
        Task<AccountType> GetById(int id);
    }
}
