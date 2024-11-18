using Application.Response;
using Domain.Models;

namespace Application.Mappers.IMappers
{
    public interface IGenericMapper
    {

        Task<GenericResponse> GetGenericResponseForAccountType(AccountType accountType);
        Task<List<GenericResponse>> GetAllGenericResponseForAccountType(List<AccountType> accountTypes);

        Task<GenericResponse> GetGenericResponseForStateAccount(StateAccount stateAccount);
        Task<List<GenericResponse>> GetAllGenericResponseForStateAccount(List<StateAccount> stateAccounts);

        Task<GenericResponse> GetGenericResponseForTypeCurrency(TypeCurrency typeCurrency);
        Task<List<GenericResponse>> GetAllGenericResponseForTypeCurrencies(List<TypeCurrency> typeCurrencies);
    }
}
