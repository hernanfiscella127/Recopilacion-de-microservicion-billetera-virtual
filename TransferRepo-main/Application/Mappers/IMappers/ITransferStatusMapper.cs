using Application.Response;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers.IMappers
{
    public interface ITransferStatusMapper
    {
        Task<List<TransferStatusResponse>> GetTransferStatus(List<TransferStatus> transfers);
        Task<TransferStatusResponse> GetOneTransferStatus(TransferStatus transfers);

    }
}
