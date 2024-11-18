using Application.Interfaces.IAccountType;
using Application.Mappers.IMappers;
using Application.Response;
using Domain.Models;

namespace Application.UseCases
{
    public class AccountTypeServices : IAccountTypeServices
    {
        private readonly IAccountTypeQuery _query;
        private readonly IGenericMapper _genericMapper;

        public AccountTypeServices(IAccountTypeQuery query, IGenericMapper genericMapper)
        {
            _query = query;
            _genericMapper = genericMapper;
        }

        public async Task<List<GenericResponse>> GetAllAccountTypes()
        {
            var accountTypes = await _query.GetAllAccountTypes();
            return await _genericMapper.GetAllGenericResponseForAccountType(accountTypes);
        }

        public async Task<AccountType> GetById(int id)
        {
            var accountType = await _query.GetAccountType(id);
            return accountType;
        }
    }
}
