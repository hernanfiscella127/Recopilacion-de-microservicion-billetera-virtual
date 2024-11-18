using Account.API.Infrastructure;
using Application.Interfaces.ITypeCurrency;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountInfrastructure.Query
{
    public class TypeCurrencyQuery : ITypeCurrencyQuery
    {
        private readonly AccountContext _context;

        public TypeCurrencyQuery(AccountContext context)
        {
            _context = context;
        }

        public async Task<TypeCurrency> GetTypeCurrency(int id)
        {
            var typeCurrency = await _context.TypeCurrency
                .FirstOrDefaultAsync(x => x.Id == id);

            return typeCurrency;
        }

        public async Task<List<TypeCurrency>> GetAllTypeCurrencies()
        {
            var results = await _context.TypeCurrency.ToListAsync();
            return results;
        }

    }

}
