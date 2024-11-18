using Application.Interfaces;
using Application.Mappers.IMappers;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class TransferStatusServices : ITransferStatusServices
    {
        private readonly ITransferStatusQuery _query;
        private readonly ITransferStatusMapper _mapper;
        public TransferStatusServices(ITransferStatusQuery query, ITransferStatusMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        public async Task<List<TransferStatusResponse>> GetAll()
        {
            var transferTypes = await _query.GetTransferStatus();
            return await _mapper.GetTransferStatus(transferTypes);
        }
    }
}
