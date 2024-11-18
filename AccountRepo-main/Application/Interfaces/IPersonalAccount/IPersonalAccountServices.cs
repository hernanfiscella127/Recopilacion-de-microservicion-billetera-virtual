using Application.Request;
using Application.Response;

namespace Application.Interfaces.IPersonalAccount
{
    public interface IPersonalAccountServices
    {
        Task<PersonalAccountResponse> AddPersonalAccount(PersonalAccountRequest personalAccountRequest);
        Task<PersonalAccountResponse> GetById(Guid id);
        Task DeletePersonalAccount(Guid id);
    }
}
