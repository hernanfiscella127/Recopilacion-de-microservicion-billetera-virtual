using Application.Exceptions;
using Application.Interfaces.IAccountModel;
using Application.Interfaces.IAccountType;
using Application.Interfaces.IHttpServices;
using Application.Interfaces.IStateAccount;
using Application.Interfaces.ITypeCurrency;
using Application.Request;
using Application.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Serilog;


namespace Application.UseCases
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountCommand _accountCommand;
        private readonly IAccountQuery _accountQuery;
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly ITypeCurrencyServices _typeCurrencyServices;
        private readonly IStateAccountServices _stateAccountServices;

        private readonly ILogger<AccountServices> _logger;

        private readonly IUserHttpService _userHttpService;
        private readonly ITransferHttpService _transferHttpService;

        private readonly Random _random;
        public AccountServices(IAccountCommand accountCommand,
            IAccountQuery accountQuery,
            IAccountTypeServices accountTypeServices,
            ITypeCurrencyServices typeCurrencyServices,
            IStateAccountServices stateAccountServices,
            ITransferHttpService transferHttpService,
            IUserHttpService userHttpService, ILogger<AccountServices> logger)
        {
            _accountCommand = accountCommand;
            _accountQuery = accountQuery;
            _accountTypeServices = accountTypeServices;
            _typeCurrencyServices = typeCurrencyServices;
            _stateAccountServices = stateAccountServices;
            _transferHttpService = transferHttpService;
            _userHttpService = userHttpService;
            _logger = logger;

            _random = new Random();
        }

        public async Task<AccountResponse> CreateAccount(int userId, AccountCreateRequest accountRequest)
        {
            //verifica si ya existe una cuenta con ese usuario
            if (await _accountQuery.UserExists(accountRequest.User))
            {
                _logger.LogInformation("This user already have an account {Time}", DateTime.UtcNow);

                throw new Conflict("This user already have an account");
            }

            string accountNumber = await GenerateAccountNumber();

            string cbu = await GenerateCBU();

            string alias = await GenerateAlias();

            var newAccountId = Guid.NewGuid();
            var account = new AccountModel
            {
                AccountId = newAccountId,
                CBU = cbu,
                Alias = alias,
                NumberAccount = accountNumber,
                Balance = 10000,  // Arranca con plata en 0 a menos definamos promos (sugerencia lucas)
                UserId = userId,
                AccTypeId = accountRequest.AccountType,
                CurrencyId = accountRequest.Currency,
                StateId = 1 //Por defecto la iniciamos activa
            };

            _logger.LogInformation("Adding account {Time}", DateTime.UtcNow);

            await _accountCommand.InsertAccount(account);

            var response = new AccountResponse
            {
                AccountId = newAccountId,
                CBU = account.CBU,
                Alias = account.Alias,
                NumeroDeCuenta = account.NumberAccount,
                Balance = account.Balance,
                TipoDeCuenta = _accountTypeServices.GetById(account.AccTypeId).Result.Name,
                TipoDeMoneda = _typeCurrencyServices.GetById(account.CurrencyId).Result.Name,
                EstadoDeLaCuenta = _stateAccountServices.GetById(account.StateId).Result.Name
            };

            return response;
        }

        public async Task<string> GenerateAccountNumber()
        {
            const int max = 20;
            int attempts = 0;

            string bankCode = "4748";
            string accountNumber;

            do
            {
                string sequentialNumber = _random.Next(10000000, 99999999).ToString();
                accountNumber = $"{bankCode}-{sequentialNumber}";
                attempts++;

                if (attempts == max)
                {
                    throw new InvalidOperationException("Account number could not be generated");
                }

                // Verifica que el numero de cuenta sea unico.
            } while (!await _accountQuery.IsAccountNumberUnique(accountNumber));

            return accountNumber;
        }

        public async Task<string> GenerateCBU()
        {
            const int max = 20;
            int attempts = 0;

            string bankCode = "28505909";
            string cbu;

            do
            {
                string randomSequence = _random.Next(100000000, 999999999).ToString() + _random.Next(10000, 99999).ToString();
                cbu = $"{bankCode}{randomSequence}";
                attempts++;

                if (attempts == max)
                {
                    throw new InvalidOperationException("CBU could not be generated");
                }

                // Verifica que el CBU sea unico.
            } while (!await _accountQuery.IsCbuUnique(cbu));

            return cbu;
        }

        public async Task<string> GenerateAlias()
        {
            const int max = 20;
            int attempts = 0;
            string alias;

            var words = new List<string>
            {
                "águila", "león", "tigre", "luna", "río", "nube", "piedra", "bosque", "montaña", "lobo",
                "sol", "estrella", "océano", "sueño", "fuego", "tormenta", "árbol", "cielo", "viento", "sombra",
                "mar", "trueno", "nieve", "rayo", "flor", "campo", "jardín", "ciudad", "desierto", "isla"
            };

            do
            {
                string word1 = words[_random.Next(words.Count)];
                string word2 = words[_random.Next(words.Count)];
                string word3 = words[_random.Next(words.Count)];

                alias = $"{word1}.{word2}.{word3}";
                attempts++;

                if (attempts == max)
                {
                    throw new InvalidOperationException("Alias could not be generated");
                }

                //Verifica que el alias sea único.
            } while (!await _accountQuery.IsAliasUnique(alias));

            return alias;
        }

        public async Task<AccountResponse> GetById(Guid id)
        {
            var account = await _accountQuery.GetAccountById(id);
            if (account == null)
            {
                return null;
            }

            var response = new AccountResponse
            {
                AccountId = account.AccountId,
                CBU = account.CBU,
                Alias = account.Alias,
                NumeroDeCuenta = account.NumberAccount,
                Balance = account.Balance,
                TipoDeCuenta = _accountTypeServices.GetById(account.AccTypeId).Result.Name,
                TipoDeMoneda = _typeCurrencyServices.GetById(account.CurrencyId).Result.Name,
                EstadoDeLaCuenta = _stateAccountServices.GetById(account.StateId).Result.Name
            };

            return response;
        }
        public async Task<AccountResponse> GetByAliasOrCBU(string searchParam)
        {
            var account = await _accountQuery.GetAccountByAliasOrCBU(searchParam);

            var response = new AccountResponse
            {
                AccountId = account.AccountId,
                CBU = account.CBU,
                Alias = account.Alias,
                NumeroDeCuenta = account.NumberAccount,
                Balance = account.Balance,
                TipoDeCuenta = _accountTypeServices.GetById(account.AccTypeId).Result.Name,
                TipoDeMoneda = _typeCurrencyServices.GetById(account.CurrencyId).Result.Name,
                EstadoDeLaCuenta = _stateAccountServices.GetById(account.StateId).Result.Name
            };

            return response;
        }
        public async Task<AccountResponse> UpdateAccount(Guid id, AccountUpdateRequest accountRequest)
        {
            var account = await _accountQuery.GetAccountById(id);
            if (account == null)
            {
                return null;
            }

            // Actualiza los campos que no son nulos
            if (!string.IsNullOrWhiteSpace(accountRequest.Alias))
            {
                if (!await _accountQuery.IsAliasUnique(accountRequest.Alias))
                {
                    throw new Conflict("This alias already exist.");
                }
                account.Alias = accountRequest.Alias;
                _logger.LogInformation("account alias {Time}", DateTime.UtcNow);
            }

            if ((accountRequest.Currency.HasValue) && (accountRequest.Currency.Value > 0))
            {
                account.CurrencyId = accountRequest.Currency.Value;
                _logger.LogInformation("acount currency {Time}", DateTime.UtcNow);
            }

            if ((accountRequest.State.HasValue) && (accountRequest.State.Value > 0))
            {
                account.StateId = accountRequest.State.Value;
                _logger.LogInformation("account state {Time}", DateTime.UtcNow);
            }

            if ((accountRequest.AccountType.HasValue) && (accountRequest.AccountType.Value > 0))
            {
                account.AccTypeId = accountRequest.AccountType.Value;
                _logger.LogInformation("account type {Time}", DateTime.UtcNow);
            }

            _logger.LogInformation("Updating account {Time}", DateTime.UtcNow);

            await _accountCommand.UpdateAccount(account);

            var response = new AccountResponse
            {
                AccountId = account.AccountId,
                CBU = account.CBU,
                Alias = account.Alias,
                NumeroDeCuenta = account.NumberAccount,
                Balance = account.Balance,
                TipoDeCuenta = _accountTypeServices.GetById(account.AccTypeId).Result.Name,
                TipoDeMoneda = _typeCurrencyServices.GetById(account.CurrencyId).Result.Name,
                EstadoDeLaCuenta = _stateAccountServices.GetById(account.StateId).Result.Name
            };

            return response;
        }


        public async Task<AccountResponse> DisableAccountByUser(int userId)
        {
            //verifica que el usuario tenga una cuenta
            if (!await _accountQuery.UserExists(userId))
            {
                return null;
            }

            _logger.LogInformation("Searching account {Time}", DateTime.UtcNow);

            var account = await _accountQuery.GetAccountByUser(userId);

            account.StateId = 3;

            _logger.LogInformation("Updating account {Time}", DateTime.UtcNow);

            await _accountCommand.UpdateAccount(account);

            var response = new AccountResponse
            {
                AccountId = account.AccountId,
                CBU = account.CBU,
                Alias = account.Alias,
                NumeroDeCuenta = account.NumberAccount,
                Balance = account.Balance,
                TipoDeCuenta = _accountTypeServices.GetById(account.AccTypeId).Result.Name,
                TipoDeMoneda = _typeCurrencyServices.GetById(account.CurrencyId).Result.Name,
                EstadoDeLaCuenta = _stateAccountServices.GetById(account.StateId).Result.Name
            };

            return response;
        }

        public async Task<TransferProcess> UpdateBalance(Guid id, AccountBalanceRequest balance)
        {
            var account = await _accountQuery.GetAccountById(id);
            if (account == null)
            {
                return null;         
            }
            else
            {
                _logger.LogInformation("Updating balance account {Time}", DateTime.UtcNow);

                if (balance.Option) //Si la opcion es true, suma el balance
                {
                    account.Balance += balance.Balance;
                    await _accountCommand.UpdateAccount(account); 

                    return new TransferProcess { TransferCompleted = true, CurrentBalance = account.Balance };
                }

                // Si es falso, resta el balance
                account.Balance -= balance.Balance;
                await _accountCommand.UpdateAccount(account);

                return new TransferProcess { TransferCompleted = true, CurrentBalance = account.Balance };

            }
        }
        
        public async Task<AccountResponse> GetByUserId(int userId)
        {
            _logger.LogInformation("Cheking if user already exist {Time}", DateTime.UtcNow);

            if (!await _accountQuery.UserExists(userId))
            {
                return null;
            }

            var account = await _accountQuery.GetAccountByUser(userId);

            _logger.LogInformation("Searching account {Time}", DateTime.UtcNow);

            var response = new AccountResponse
            {
                AccountId = account.AccountId,
                CBU = account.CBU,
                Alias = account.Alias,
                NumeroDeCuenta = account.NumberAccount,
                Balance = account.Balance,
                TipoDeCuenta = _accountTypeServices.GetById(account.AccTypeId).Result.Name,
                TipoDeMoneda = _typeCurrencyServices.GetById(account.CurrencyId).Result.Name,
                EstadoDeLaCuenta = _stateAccountServices.GetById(account.StateId).Result.Name
            };

            return response;
        }
    }
}
