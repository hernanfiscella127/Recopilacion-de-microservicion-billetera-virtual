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
    public class TransferTypeServices : ITransferTypeServices
    {
        private readonly ITransferTypeQuery _query;
        private readonly ITransferTypeMapper _mapper;
        public TransferTypeServices(ITransferTypeQuery query, ITransferTypeMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        public async Task<List<TransferTypeResponse>> GetAll()
        {
            var transferTypes = await _query.GetTransferTypes();
            return await _mapper.GetTransferTypes(transferTypes);
        }
    }
}
