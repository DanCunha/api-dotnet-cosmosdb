using ChallengeAPI.Models.Entities;
using ChallengeAPI.Repository.Interfaces;
using ChallengeAPI.Service.Interfaces;

namespace ChallengeAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetProductsAsync();
        }

        public async Task SaveAsync(Product product)
        {
            await _repository.CreateItemAsync(product);
        }
    }
}
