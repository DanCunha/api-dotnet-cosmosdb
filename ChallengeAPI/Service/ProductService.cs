using ChallengeAPI.AzureServices;
using ChallengeAPI.AzureServices.Interfaces;
using ChallengeAPI.Models.Entities;
using ChallengeAPI.Repository.Interfaces;
using ChallengeAPI.Service.Interfaces;

namespace ChallengeAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductQueue _productQueue;

        public ProductService(IProductRepository repository, IProductQueue productQueue)
        {
            _repository = repository;
            _productQueue = productQueue;   
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetProductsAsync();
        }

        public async Task<Product> SaveAsync(Product product)
        {

            var response = await _repository.CreateItemAsync(product);
            Console.WriteLine("CreateItemAsync");
            await _productQueue.SendMessage(product);
            Console.WriteLine("SendMessage");

            return response;
        }
    }
}
