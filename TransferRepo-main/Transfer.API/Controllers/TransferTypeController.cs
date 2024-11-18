using Application.Interfaces;
using Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Transfer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferTypeController : ControllerBase
    {
        private readonly ITransferTypeServices _services;

        public TransferTypeController(ITransferTypeServices services, HttpClient httpClient)
        {
            _services = services;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TransferTypeResponse>), 200)]
        public async Task<IActionResult> GetTransferStatuses()
        {
            var result = await _services.GetAll();
            return new JsonResult(result) { StatusCode = 200 };
        }

    }
}
