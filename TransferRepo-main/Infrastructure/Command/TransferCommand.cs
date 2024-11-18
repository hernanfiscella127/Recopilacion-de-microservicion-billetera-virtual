using Application.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class TransferCommand : ITransferCommand
    {
        private readonly TransferContext _context;
        public TransferCommand(TransferContext context)
        {
            _context = context;
        }

        public async Task DeleteTransfer(Guid transferid)
        {
            var tranfer = await _context.Transfers.FindAsync(transferid);
            tranfer.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task InsertTransfer(Transfer transfer)
        {
            _context.Add(transfer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransfer(Transfer transfer)
        {
            _context.Update(transfer);
            await _context.SaveChangesAsync();
        }
    }
}
