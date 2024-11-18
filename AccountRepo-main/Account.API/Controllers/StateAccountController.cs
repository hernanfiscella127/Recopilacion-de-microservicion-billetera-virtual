using Application.Interfaces.IStateAccount;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateAccountController : Controller
    {
        private readonly IStateAccountServices _stateAccountServices;
        private readonly ILogger<StateAccountController> _logger;

        public StateAccountController(IStateAccountServices stateAccountServices, ILogger<StateAccountController> logger)
        {
            _stateAccountServices = stateAccountServices;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all state account {Time}", DateTime.UtcNow);

            var result = await _stateAccountServices.GetAllStateAccounts();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}
