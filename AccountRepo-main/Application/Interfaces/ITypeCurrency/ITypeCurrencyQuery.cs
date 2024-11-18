using Domain.Models;

namespace Application.Interfaces.ITypeCurrency
{
    public interface ITypeCurrencyQuery
    {
        Task<List<TypeCurrency>> GetAllTypeCurrencies();
        Task<TypeCurrency> GetTypeCurrency(int id);
    }
}
