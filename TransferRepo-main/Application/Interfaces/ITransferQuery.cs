using Domain.Models;

namespace Application.Interfaces
{
    public interface ITransferQuery
    {
        Task<List<Transfer>> GetUserTransfers(Guid UserId);
        Task<Transfer> GetTransferById(Guid id);
        Task<List<Transfer>> GetAll();
        Task<List<Transfer>> GetTransferByFilter(Guid? UserId, decimal? amount, DateTime? date, int? type, int? status);
        Task<List<Transfer>> GetTransfersByAccount(Guid accountId, int? offset, int? size);
    }
}
