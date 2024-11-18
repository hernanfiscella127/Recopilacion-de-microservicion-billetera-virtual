using Application.Interfaces.ITypeCurrency;
using Application.Mappers.IMappers;
using Application.Response;
using Domain.Models;

namespace Application.UseCases
{
    public class TypeCurrencyServices : ITypeCurrencyServices
    {
        private readonly ITypeCurrencyQuery _query;
        private readonly IGenericMapper _genericMapper;

        public TypeCurrencyServices(ITypeCurrencyQuery query, IGenericMapper genericMapper)
        {
            _query = query;
            _genericMapper = genericMapper;
        }
        public async Task<List<GenericResponse>> GetAllTypeCurrencies()
        {
            var typeCurrencies = await _query.GetAllTypeCurrencies();
            return await _genericMapper.GetAllGenericResponseForTypeCurrencies(typeCurrencies);
        }

        public async Task<TypeCurrency> GetById(int id)
        {
            var typeCurrency = await _query.GetTypeCurrency(id);
            return typeCurrency;
        }
    }
}
