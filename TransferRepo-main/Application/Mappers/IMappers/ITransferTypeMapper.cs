using Application.Response;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers.IMappers
{
    public interface ITransferTypeMapper
    {
        Task<List<TransferTypeResponse>> GetTransferTypes(List<TransferType> transfers);
    }
}
