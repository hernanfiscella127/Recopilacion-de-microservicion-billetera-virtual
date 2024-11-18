using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers.IMappers;
using Application.Request;
using Application.Response;
using Azure.Core;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.UseCases
{
    public class TransferServices : ITransferServices
    {
        private readonly ITransferCommand _command;
        private readonly ITransferQuery _query;
        private readonly ITransferMapper _mapper;
        private readonly IAccountHttpService _accountHttpService;

        public TransferServices(ITransferCommand command, ITransferQuery query, ITransferMapper mapper,IAccountHttpService accountHttpService)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _accountHttpService = accountHttpService;
        }
        public async Task<TransferResponse> CreateTransfer(CreateTransferRequest request)
        {
            var accountSrc = await _accountHttpService.GetAccountById(request.SrcAccountId)
            ?? throw new Conflict("Source account not found");

            //var accountDest = await _accountHttpService.GetAccountById(request.DestAccountId)
            //?? throw new Conflict("Destine account not found");

            var accountDest = await _accountHttpService.GetAccountByAliasOrCBU(request.DestAccountAliasOrCBU)
            ?? throw new Conflict("Destine account not found");

            var accountTransfers = await _query.GetTransferByFilter(accountSrc.AccountId, null, null, null, 1);
            if (accountTransfers.Count() != 0)
            {
                throw new AccountErrorException("You have a transference in course, please wait untill it is finished");
            }

            var transfer = new Transfer
            {
                Amount = request.Amount,
                Date = DateTime.Now,
                StatusId = 1,
                Description = request.Description,
                TypeId = request.TypeId,
                SrcAccountId = request.SrcAccountId,
                DestAccountId = accountDest.AccountId,
                DestAccountAliasOrCBU = accountDest.Alias,
            };

            if (accountDest.EstadoDeLaCuenta == "Active" && accountSrc.EstadoDeLaCuenta == "Active")
            {
                    if (accountDest.Balance < transfer.Amount) {
                        transfer.StatusId = 3;
                        await _command.UpdateTransfer(transfer);
                        throw new AccountErrorException("Not enough money to make the transaction");
                    }
                    else {
                        await _command.InsertTransfer(transfer);
                        var balanceData1 = new AccountBalanceRequest
                        {
                            Option = false,
                            Balance = request.Amount
                        };
                        var response1 = await _accountHttpService.UpdateAccountBalance(accountSrc.AccountId, balanceData1);

                        var balanceData2 = new AccountBalanceRequest
                        {
                            Option = true,
                            Balance = request.Amount
                        };
                        var response2 = await _accountHttpService.UpdateAccountBalance(accountDest.AccountId, balanceData2);

                        transfer.StatusId = 2;
                        await _command.UpdateTransfer(transfer);
                    }
            }
            else {
                transfer.StatusId = 3;
                await _command.UpdateTransfer(transfer);
                throw new Conflict("Transference canceled for 'internal' issues");
            }
            return await _mapper.GetOneTransfer(await _query.GetTransferById(transfer.Id));
        }

        public async Task<TransferResponse> UpdateTransfer(UpdateTransferRequest request, Guid transferId)
        {

            var tranfer = await _query.GetTransferById(transferId);
            if (tranfer == null) 
            {
                throw new ExceptionNotFound("There's no transfer with that Id");
            }
            await _command.UpdateTransfer(tranfer);
            
            return await _mapper.GetOneTransfer(await _query.GetTransferById(tranfer.Id));
        }

        public async Task<TransferResponse> DeleteTransfer(Guid transfer)
        {
            if (transfer == null)
            {
                throw new ExceptionNotFound("There's no transfer with that Id");
            }
            var response = await _query.GetTransferById(transfer);
            await _command.DeleteTransfer(transfer);

            return await _mapper.GetOneTransfer(response);
        }

        public async Task<TransferResponse>GetTransferById(Guid transferId)
        {
            if (transferId == null)
            {
                throw new ExceptionNotFound("There's no transfer with that Id");
            }
            var response = await _query.GetTransferById(transferId);
            return await _mapper.GetOneTransfer(response);
        }

        public async Task<List<TransferResponse>> GetAll()
        {
            var transfers = await _query.GetAll();
            return await _mapper.GetTransfers(transfers);
        }

        public async Task<List<TransferResponse>> GetAllByUser(Guid UserId)
        {
            var user1 = await _accountHttpService.GetAccountById(UserId)
            ?? throw new Conflict("User not found");
            var userTransfers = await _query.GetUserTransfers(UserId);
            return await _mapper.GetTransfers(userTransfers);
        }

        public async Task<List<TransferResponse>> GetAllByAccount(Guid accountId, int? offset, int? size)
        {
            var transfers = await _query.GetTransfersByAccount(accountId, offset, size)
                ?? throw new Conflict("Account not found");
            foreach (var transfer in transfers)
            {
                if (transfer.SrcAccountId == accountId) { 
                    transfer.Amount = transfer.Amount * (-1);
                }
            }          
            return await _mapper.GetTransfers(transfers);
        }
    }
}
