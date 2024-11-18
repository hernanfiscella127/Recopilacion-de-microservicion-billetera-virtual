using Application.Interfaces.IPersonalAccount;
using Application.Request;
using Application.Response;

namespace Application.UseCases
{
    public class PersonalAccountServices : IPersonalAccountServices
    {
        public Task<PersonalAccountResponse> AddPersonalAccount(PersonalAccountRequest personalAccountRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeletePersonalAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonalAccountResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
