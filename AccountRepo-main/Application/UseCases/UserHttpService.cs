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
    public class UserHttpService : IUserHttpService
    {
        private readonly HttpClient _httpClient;

        public UserHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserResponse> GetUserById(int userId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7160/api/User/{userId}");
            //var x = 1;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponse>();
            }

            return null; // Manejar errores de forma apropiada
        }
    }
}
