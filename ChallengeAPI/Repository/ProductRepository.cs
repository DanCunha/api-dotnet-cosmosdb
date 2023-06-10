using ChallengeAPI.Context;
using ChallengeAPI.Models.Dtos;
using ChallengeAPI.Models.Entities;
using ChallengeAPI.Repository.Interfaces;
using Microsoft.Azure.Cosmos;

namespace ChallengeAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ChallengeContext _context;

        public ProductRepository(ChallengeContext context)
        {
            _context = context;
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var querDefinition = "SELECT * FROM c";

            List<Product> results = new List<Product>();

            var container = await _context.GetContainer();
            var query = container.GetItemQueryIterator<Product>(new QueryDefinition(querDefinition));
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Product> CreateItemAsync(Product product)
        {
            var container = await _context.GetContainer();
            ItemResponse<Product> productResponse = await container.CreateItemAsync<Product>(product, new PartitionKey(product.PartitionKey));

            return productResponse;
        }

        public async Task<Product> UpdateItemAsync(Product product)
        {
            var container = await _context.GetContainer();
            ItemResponse<Product> productResponse = await container.ReadItemAsync<Product>(product.Id, new PartitionKey(product.PartitionKey));
            var itemBody = productResponse.Resource;

            // replace the item with the updated content
            productResponse = await container.ReplaceItemAsync<Product>(itemBody, itemBody.Id, new PartitionKey(itemBody.PartitionKey));
            Console.WriteLine("Updated Family [{0},{1}].\n \tBody is now: {2}\n", itemBody.Name, itemBody.Id, productResponse.Resource);

            return productResponse;
        }
    }
}
