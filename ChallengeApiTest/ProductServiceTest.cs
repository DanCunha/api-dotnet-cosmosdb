using ChallengeAPI.AzureServices;
using ChallengeAPI.AzureServices.Interfaces;
using ChallengeAPI.Models.Entities;
using ChallengeAPI.Repository.Interfaces;
using ChallengeAPI.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeApiTest
{
    public class ProductServiceTest
    {
        private ProductService productService;

        public ProductServiceTest()
        {
            productService = new ProductService(new Mock<IProductRepository>().Object, new Mock<IProductQueue>().Object);
        }

        [Fact]
        public async Task CreateItem()
        {
            Product product = new Product()
            {
                Id = "1",
                Name = "Teste",
                Description = "Description",    
                Brand = "Brand",
                Qtd = 10
            };

            var response = productService.SaveAsync(product);
            Assert.True(response != null);
        }
    }
}
