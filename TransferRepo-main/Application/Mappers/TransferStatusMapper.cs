using Application.Mappers.IMappers;
using Application.Response;
using Azure;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class TransferStatusMapper : ITransferStatusMapper
    {
        public async Task<TransferStatusResponse> GetOneTransferStatus(TransferStatus transfer)
        {
            var response = new TransferStatusResponse
            {
                Id = transfer.TransferStatusId,
                Status = transfer.Status,
            };
            return response;
        }

        public async Task<List<TransferStatusResponse>> GetTransferStatus(List<TransferStatus> transfers)
        {
            List<TransferStatusResponse> responses = new List<TransferStatusResponse>();
            foreach (var transfer in transfers)
            {
                var response = new TransferStatusResponse
                {
                    Id = transfer.TransferStatusId,
                    Status = transfer.Status,
                };
                responses.Add(response);
            }
            return responses;
        }
    }
}
