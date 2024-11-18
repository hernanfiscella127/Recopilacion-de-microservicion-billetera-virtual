using Application.Interfaces.ITypeCurrency;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeCurrencyController : Controller
    {
        private readonly ITypeCurrencyServices _typeCurrencyServices;
        private readonly ILogger<TypeCurrencyController> _logger;

        public TypeCurrencyController(ITypeCurrencyServices typeCurrencyServices, ILogger<TypeCurrencyController> logger)
        {
            _typeCurrencyServices = typeCurrencyServices;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all Type currency {Time}", DateTime.UtcNow);

            var result = await _typeCurrencyServices.GetAllTypeCurrencies();
            return new JsonResult(result) { StatusCode = 200 };
        }

    }
}
