using Application.Response;
using Domain.Models;

namespace Application.Mappers.IMappers
{
    public interface ITransferMapper
    {
        Task<TransferResponse> GetOneTransfer(Transfer transfer);
        Task<List<TransferResponse>> GetTransfers(List<Transfer> transfers);
    }
}
