using ChallengeAPI.Models.Dtos;
using ChallengeAPI.Models.Entities;

namespace ChallengeAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);

        Task<Product> CreateItemAsync(Product product);
    }
}
