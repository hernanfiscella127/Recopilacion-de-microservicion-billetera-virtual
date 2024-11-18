using Application.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class TransferStatusQuery : ITransferStatusQuery
    {
        private readonly TransferContext _context;
        public TransferStatusQuery(TransferContext context)
        {
            _context = context;
        }
        public async Task<List<TransferStatus>> GetTransferStatus()
        {
            var transfers = await _context.TransferStatuses.ToListAsync();
            return transfers;
        }
    }
}
