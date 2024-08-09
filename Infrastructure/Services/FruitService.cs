using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class FruitService : IFruitService
    {
        private readonly IFruityviceApiClient _apiClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<FruitService> _logger;

        private const string FruitCacheKey = "fruits";

        public FruitService(IFruityviceApiClient apiClient, IMemoryCache cache, ILogger<FruitService> logger)
        {
            _apiClient = apiClient;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<Fruit>> GetAllFruitsAsync()
        {
            if (!_cache.TryGetValue(FruitCacheKey, out IEnumerable<Fruit> fruits))
            {
                try
                {
                    fruits = await _apiClient.GetAllFruitsAsync();
                    CacheFruits(fruits);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching fruits from API");
                    throw new ApplicationException("Error fetching fruits from API", ex);
                }
            }

            return fruits;
        }

        public async Task<Fruit> GetFruitByNameAsync(string name)
        {
            var fruits = await GetAllFruitsAsync();
            return fruits.FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task AddMetadataAsync(string name, string key, decimal value)
        {
            await UpdateFruitMetadataAsync(name, (nutritions) => AddOrUpdateNutrition(nutritions, key, value));
        }

        public async Task RemoveMetadataAsync(string name, string key)
        {
            await UpdateFruitMetadataAsync(name, (nutritions) => RemoveNutrition(nutritions, key));
        }

        public async Task UpdateMetadataAsync(string name, string key, decimal value)
        {
            await UpdateFruitMetadataAsync(name, (nutritions) => AddOrUpdateNutrition(nutritions, key, value));
        }

        private async Task UpdateFruitMetadataAsync(string name, Action<Dictionary<string, decimal>> updateAction)
        {
            var fruits = await GetAllFruitsAsync();
            var fruit = fruits.FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (fruit == null)
            {
                throw new KeyNotFoundException($"Fruit '{name}' not found.");
            }

            updateAction(fruit.Nutritions ??= new Dictionary<string, decimal>());
            UpdateCache(fruits);
        }

        private void AddOrUpdateNutrition(Dictionary<string, decimal> nutritions, string key, decimal value)
        {
            if (nutritions == null)
            {
                nutritions = new Dictionary<string, decimal>();
            }

            nutritions[key] = value;
        }

        private void RemoveNutrition(Dictionary<string, decimal> nutritions, string key)
        {
            if (nutritions != null && nutritions.ContainsKey(key))
            {
                nutritions.Remove(key);
            }
        }

        private void UpdateCache(IEnumerable<Fruit> fruits)
        {
            CacheFruits(fruits);
        }

        private void CacheFruits(IEnumerable<Fruit> fruits)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };
            _cache.Set(FruitCacheKey, fruits, cacheEntryOptions);
        }
    }
}
