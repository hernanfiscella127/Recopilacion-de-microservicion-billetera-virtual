using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Response;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class TransferTypeQuery : ITransferTypeQuery
    {
        private readonly TransferContext _context;
        public TransferTypeQuery(TransferContext context)
        {
            _context = context;
        }
        public async Task<List<TransferType>> GetTransferTypes()
        {
            var transfers = await _context.TransferTypes.ToListAsync();
            return transfers;
        }
    }
}
