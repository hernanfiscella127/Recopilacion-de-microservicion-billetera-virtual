using Application.Interfaces.IStateAccount;
using Application.Mappers.IMappers;
using Application.Response;
using Domain.Models;

namespace Application.UseCases
{
    public class StateAccountServices : IStateAccountServices
    {
        private readonly IStateAccountQuery _query;
        private readonly IGenericMapper _genericMapper;

        public StateAccountServices(IStateAccountQuery query, IGenericMapper genericMapper)
        {
            _query = query;
            _genericMapper = genericMapper;
        }
        public async Task<List<GenericResponse>> GetAllStateAccounts()
        {
            var stateAccounts = await _query.GetAllStateAccounts();
            return await _genericMapper.GetAllGenericResponseForStateAccount(stateAccounts);
        }

        public async Task<StateAccount> GetById(int id)
        {
            var stateAccount = await _query.GetStateAccount(id);
            return stateAccount;
        }
    }
}
