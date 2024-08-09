using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Infrastructure.Clients
{
    public class FruityviceApiClient : IFruityviceApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public FruityviceApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["FruityviceApi:BaseUrl"];
        }

        public async Task<IEnumerable<Fruit>> GetAllFruitsAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}all");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Fruit>>();
        }
    }

}
