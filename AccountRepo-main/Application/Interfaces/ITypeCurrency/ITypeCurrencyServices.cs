using Application.Response;
using Domain.Models;

namespace Application.Interfaces.ITypeCurrency
{
    public interface ITypeCurrencyServices
    {
        Task<List<GenericResponse>> GetAllTypeCurrencies();
        Task<TypeCurrency> GetById(int id);
    }
}
