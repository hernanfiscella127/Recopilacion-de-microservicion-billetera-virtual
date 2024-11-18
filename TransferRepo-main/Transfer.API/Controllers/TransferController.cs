using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Transfer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferServices _services;

        public TransferController(ITransferServices services, HttpClient httpClient)
        {
            _services = services;
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(TransferResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult>DeleteTransfer(Guid Id)
        {
            try
            {
                var result = await _services.DeleteTransfer(Id);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionNotFound ex)
            {

                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 }; ;
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<TransferResponse>), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> GetTransferById(Guid transferId)
        {
            try
            {
                var result = await _services.GetTransferById(transferId);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionNotFound ex)
            {

                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 }; ;
            }
        }

        [HttpGet("{id}/Accounts")]
        [ProducesResponseType(typeof(List<TransferResponse>), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult>GetAllTransfersBySrcAccountId(Guid id, int? offset, int? size)
        {
            try
            {
                var result = await _services.GetAllByAccount(id, offset, size);
                return new JsonResult(result) { StatusCode= 200};
            }
            catch (Conflict ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(TransferResponse), 201)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> CreateTransfer(CreateTransferRequest request)
        {
            try
            {
                var result = await _services.CreateTransfer(request);
                return new JsonResult(result)
                {
                    StatusCode = 201
                };
            }
            catch (Conflict ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
            }
            catch (AccountErrorException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(TransferResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> UpdateTransfer(UpdateTransferRequest request, Guid transferId)
        {
            try
            {
                var result = await _services.UpdateTransfer(request,transferId);
                return new JsonResult(result)
                {
                    StatusCode = 200
                };
            }
            catch (Conflict ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
            }
        }
    }
}
