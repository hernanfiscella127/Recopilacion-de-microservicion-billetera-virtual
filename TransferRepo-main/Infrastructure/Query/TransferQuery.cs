using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Query
{
    public class TransferQuery : ITransferQuery
    {
        private readonly TransferContext _context;
        public TransferQuery(TransferContext context)
        {
            _context = context;
        }

        public async Task<List<Transfer>> GetAll()
        {
            var transfers = await _context.Transfers.ToListAsync();
            return transfers;
        }


        public async Task<List<Transfer>> GetTransferByFilter(Guid? UserId, decimal? amount, DateTime? date, int? type, int? status)//Añadir Alias tambien
        {

            var list =  _context.Transfers
                .Include(t => t.TransferType)
                .Include(t => t.Status)
                .AsQueryable();

            if (UserId != null) 
            {
                list = list.Where(x => x.SrcAccountId == UserId);
            }
            if (amount != null) 
            {
                list = list.Where(list => list.Amount == amount);
            }
            if (date.HasValue) 
            {
                list = list.Where(list=> list.Date == date.Value);
            }
            if (type != null)
            {
                list = list.Where(list => list.TypeId == type);
            }
            if (status != null)
            {
                list = list.Where(list => list.StatusId == status);
            }

            return await list.ToListAsync();

        }

        public async Task<Transfer> GetTransferById(Guid Id)
        {
            var project = await _context.Transfers
                .Include(t => t.TransferType)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(s => s.Id == Id);
            return project;
        }

        public async Task<List<Transfer>> GetUserTransfers(Guid UserId)
        {
            return await _context.Transfers
                .Include(t => t.TransferType)
                .Include(t => t.Status)
                .Where(t => t.SrcAccountId == UserId).ToListAsync();
        }

        public async Task<List<Transfer>> GetTransfersByAccount(Guid accountId, int? offset, int? size)
        {
            var transfers =  _context.Transfers
                .Include(t => t.TransferType)
                .Include(t => t.Status)
                .Where(t => t.SrcAccountId == accountId || t.DestAccountId == accountId)
                .OrderByDescending(t => t.Date)
                //.Skip((offset - 1) * size)
                //.Take(size)
                .AsQueryable();

            if (offset.HasValue && size.HasValue)
            {
                transfers = transfers.Skip((offset.Value - 1) * size.Value).Take(size.Value);
            }

            return await transfers.ToListAsync();
        }
    }
}
