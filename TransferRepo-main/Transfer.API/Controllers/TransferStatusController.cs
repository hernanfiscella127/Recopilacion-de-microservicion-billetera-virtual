using Application.Exceptions;
using Application.Interfaces;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Transfer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferStatusController : ControllerBase
    {
        private readonly ITransferStatusServices _services;

        public TransferStatusController(ITransferStatusServices services, HttpClient httpClient)
        {
            _services = services;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TransferStatusResponse>), 200)]
        public async Task<IActionResult> GetTransferStatuses()
        {
            var result = await _services.GetAll();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}
