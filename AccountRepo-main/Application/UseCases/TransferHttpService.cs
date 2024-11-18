using Application.Interfaces.IHttpServices;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class TransferHttpService : ITransferHttpService
    {
        private readonly HttpClient _httpClient;

        public TransferHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TransferResponse>> GetAllTransfersByAccount(Guid accountId)
        {
            
            var response = await _httpClient.GetAsync($"https://localhost:7045/api/Transfer/{accountId}/Accounts");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TransferResponse>>();
            }

            return null; //Manejar de forma correcta
            
        }
    }
}
