using Application.Mappers.IMappers;
using Application.Response;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Mappers
{
    public class GenericMapper : IGenericMapper
    {
        public Task<GenericResponse> GetGenericResponseForAccountType(AccountType accountType)
        {
            return Task.FromResult(MapToGenericResponse(accountType));
        }

        public Task<List<GenericResponse>> GetAllGenericResponseForAccountType(List<AccountType> accountTypes)
        {
            return Task.FromResult(MapToGenericResponseList(accountTypes));
        }

        public Task<GenericResponse> GetGenericResponseForStateAccount(StateAccount stateAccount)
        {
            return Task.FromResult(MapToGenericResponse(stateAccount));
        }

        public Task<List<GenericResponse>> GetAllGenericResponseForStateAccount(List<StateAccount> stateAccounts)
        {
            return Task.FromResult(MapToGenericResponseList(stateAccounts));
        }

        public Task<GenericResponse> GetGenericResponseForTypeCurrency(TypeCurrency typeCurrency)
        {
            return Task.FromResult(MapToGenericResponse(typeCurrency));
        }

        public Task<List<GenericResponse>> GetAllGenericResponseForTypeCurrencies(List<TypeCurrency> typeCurrencies)
        {
            return Task.FromResult(MapToGenericResponseList(typeCurrencies));
        }


        private GenericResponse MapToGenericResponse<T>(T entity) where T : ICommonObject
        {
            return new GenericResponse
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        private List<GenericResponse> MapToGenericResponseList<T>(List<T> entities) where T : ICommonObject
        {
            List<GenericResponse> listResponse = new List<GenericResponse>();
            foreach (var element in entities)
            {
                var response = new GenericResponse
                {
                    Id = element.Id,
                    Name = element.Name,
                };
                listResponse.Add(response);
            }
            return listResponse;
        }
    }
}
