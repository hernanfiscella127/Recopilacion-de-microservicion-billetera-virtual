using Application.Interfaces.IAccountType;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly ILogger<AccountTypeController> _logger;

        public AccountTypeController(IAccountTypeServices accountTypeServices, ILogger<AccountTypeController> logger)
        {
            _accountTypeServices = accountTypeServices;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all account types {Time}", DateTime.UtcNow);

            var result = await _accountTypeServices.GetAllAccountTypes();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}
