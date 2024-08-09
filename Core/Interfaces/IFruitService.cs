using Core.Entities;

namespace Infrastructure.Services
{
    public interface IFruitService
    {
        Task<IEnumerable<Fruit>> GetAllFruitsAsync();
        Task<Fruit> GetFruitByNameAsync(string name);
        Task AddMetadataAsync(string name, string key, decimal value);
        Task RemoveMetadataAsync(string name, string key);
        Task UpdateMetadataAsync(string name, string key, decimal value);
    }
}