using Core.Entities;

namespace Core.Interfaces
{
    public interface IFruityviceApiClient
    {
        Task<IEnumerable<Fruit>> GetAllFruitsAsync();
    }
}
