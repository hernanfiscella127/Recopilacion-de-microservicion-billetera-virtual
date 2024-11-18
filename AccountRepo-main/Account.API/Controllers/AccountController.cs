using Application.Interfaces.IAccountModel;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Account.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountServices accountServices, ILogger<AccountController> logger)
        {
            _accountServices = accountServices;
            _logger = logger;
        }
         
        /// <summary>
        /// Crea una nueva cuenta bancaria
        /// </summary>
        /// <param name="accountRequest">Objeto de solicitud que contiene la información necesaria para crear una cuenta</param>
        /// <returns>El objeto de respuesta con los detalles de la cuenta creada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccountResponse), 201)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> CreateAccount ([FromHeader] string authorization, [FromBody] AccountCreateRequest accountRequest)
        {
            _logger.LogInformation("Create account {Time}", DateTime.UtcNow);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create account/bad request {Time}", DateTime.UtcNow);
                return BadRequest(ModelState);
            }
            
            if (string.IsNullOrEmpty(authorization))
            {
                return Unauthorized("Token is required");
            }

            // Obtiene el userId desde los claims
            var userId = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            if (userId == null)
            {
                return Unauthorized("No se pudo obtener el Id de usuario desde el token");
            }

            try
            {
                var accountResponse = await _accountServices.CreateAccount(int.Parse(userId), accountRequest);

                _logger.LogInformation("Account created {Time}", DateTime.UtcNow);

                return Created(string.Empty,accountResponse);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception 500 {Time}", DateTime.UtcNow);
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene una cuenta por su ID
        /// </summary>
        /// <param name="id">El ID de la cuenta</param>
        /// <returns>La información de la cuenta solicitada, junto con el usuario y transferencias</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            _logger.LogInformation("Get account by id {Time}", DateTime.UtcNow);

            try
            {
                var accountResponse = await _accountServices.GetById(id);

                if (accountResponse == null)
                {
                    _logger.LogInformation("Account not found {Time}", DateTime.UtcNow);

                    return NotFound();
                }

                _logger.LogInformation("Account found {Time}", DateTime.UtcNow);

                return Ok(accountResponse);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception 500 {Time}", DateTime.UtcNow);

                return StatusCode(500, new { Message = ex.Message });
            }
        }


        [HttpGet("{searchParam}/Alias")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountByAliasOrCBU(string searchParam)
        {
            _logger.LogInformation("Get account by id {Time}", DateTime.UtcNow);

            try
            {
                var accountResponse = await _accountServices.GetByAliasOrCBU(searchParam);

                if (accountResponse == null)
                {
                    _logger.LogInformation("Account not found {Time}", DateTime.UtcNow);

                    return NotFound();
                }

                _logger.LogInformation("Account found {Time}", DateTime.UtcNow);

                return Ok(accountResponse);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception 500 {Time}", DateTime.UtcNow);

                return StatusCode(500, new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza una cuenta por su Id y datos ingresados
        /// </summary>
        /// <param name="accountId">El ID de la cuenta</param>
        /// <param name="accountRequest">Objeto de solicitud que contiene la información para actualizar una cuenta</param>
        /// <returns>La cuenta con los datos modificados</returns>

        [HttpPatch("Update")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAccount(Guid accountId, [FromBody] AccountUpdateRequest accountRequest)
        {
            _logger.LogInformation("Update account {Time}", DateTime.UtcNow);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update account/bad request {Time}", DateTime.UtcNow);

                return BadRequest(ModelState);
            }

            try
            {
                var result = await _accountServices.UpdateAccount(accountId, accountRequest);
                
                if (result == null)
                {
                    _logger.LogInformation("Account not found {Time}", DateTime.UtcNow);
                    return NotFound();
                }

                _logger.LogInformation("Account updated {Time}", DateTime.UtcNow);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception 500 {Time}", DateTime.UtcNow);
                return StatusCode(400, new { Message = ex.Message });
            }

        }

        [HttpPatch("Update/Balance/{accountId}")]
        [ProducesResponseType(typeof(TransferProcess), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateBalancee(Guid accountId, [FromBody] AccountBalanceRequest balance)
        {
            _logger.LogInformation("Update account balance {Time}", DateTime.UtcNow);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update account balance/bad request {Time}", DateTime.UtcNow);

                return BadRequest(ModelState);
            }

            try
            {
                var result = await _accountServices.UpdateBalance(accountId, balance);

                if (result == null)
                {
                    _logger.LogInformation("Account not found {Time}", DateTime.UtcNow);

                    return NotFound();
                }

                _logger.LogInformation("Account balance updated {Time}", DateTime.UtcNow);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception 500 {Time}", DateTime.UtcNow);
                return StatusCode(500, new { Message = ex.Message });
            }

        }

        /// <summary>
        /// Obtiene una cuenta por usuario
        /// </summary>
        /// <param name="userId">El ID de un usuario</param>
        /// <returns>La información de la cuenta solicitada, junto con el usuario y transferencias</returns>
        [HttpGet("User/{userId}")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountByUserId(int userId)
        {
            _logger.LogInformation("Get account by user id {Time}", DateTime.UtcNow);

            try
            {
                var accountResponse = await _accountServices.GetByUserId(userId);

                if (accountResponse == null)
                {
                    _logger.LogInformation("Account not found {Time}", DateTime.UtcNow);

                    return NotFound();
                }

                _logger.LogInformation("Account found {Time}", DateTime.UtcNow);

                return Ok(accountResponse);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception 500 {Time}", DateTime.UtcNow);

                return StatusCode(500, new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deshabilita una cuenta por usuario
        /// </summary>
        /// <param name="userId">El ID de un usuario</param>
        /// <returns>Informacion de la cuenta con el estado actualizado</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAccountByUser(int userId)
        {
            _logger.LogInformation("Delete account {Time}", DateTime.UtcNow);

            try
            {
                var accountResponse = await _accountServices.DisableAccountByUser(userId); ;

                if (accountResponse == null)
                {
                    _logger.LogInformation("Account not found {Time}", DateTime.UtcNow);

                    return NotFound();
                }

                _logger.LogInformation("Account deleted {Time}", DateTime.UtcNow);

                return Ok(accountResponse);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception 500 {Time}", DateTime.UtcNow);
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
