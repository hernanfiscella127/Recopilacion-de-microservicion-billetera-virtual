using Domain.Models;

namespace Application.Interfaces
{
    public interface ITransferCommand
    {
        public Task InsertTransfer(Transfer transfer);
        public Task DeleteTransfer(Guid transferid);
        public Task UpdateTransfer(Transfer transfer);
    }
}
