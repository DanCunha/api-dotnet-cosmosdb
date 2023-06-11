using ChallengeAPI.Models.Entities;

namespace ChallengeAPI.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> SaveAsync(Product product);
    }
}
