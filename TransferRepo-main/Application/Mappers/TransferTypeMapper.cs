using Application.Response;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers.IMappers
{
    public class TransferTypeMapper : ITransferTypeMapper
    {
        public async Task<List<TransferTypeResponse>> GetTransferTypes(List<TransferType> transfers)
        {
            List<TransferTypeResponse> responses = new List<TransferTypeResponse>();
            foreach (var transfer in transfers)
            {
                var response = new TransferTypeResponse
                {
                    TransferTypeId = transfer.TransferTypeId,
                    Name = transfer.Name,
                };
                responses.Add(response);
            }
            return responses;
        }
    }
}
