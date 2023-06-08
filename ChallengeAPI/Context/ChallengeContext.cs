using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace ChallengeAPI.Context
{
    public class ChallengeContext
    {
        private Database database;
        private Container container;
        private string dataBaseId = "Company";
        private string containerId = "Products";
        private static readonly string accountEndpoint = "https://664a7b32-0ee0-4-231-b9ee.documents.azure.com:443/";
        private static readonly string accountKey = "MwKCKKknXorCF0S2aE9RhC3aF9jL4UeTF6DLPSM4xZiI1dwiioB6j4fPEpXkiDWT601SZbkQS40VACDbeUVBFg==";


        public async Task<Container> GetContainer() 
        {

            var client = new CosmosClientBuilder(accountEndpoint, accountKey).Build();
            database = await client.CreateDatabaseIfNotExistsAsync(dataBaseId);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);

            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/partitionKey");
            Console.WriteLine("Created Container: {0}\n", this.container.Id);

            return container = client.GetContainer(dataBaseId, containerId);

        }   
    }
}
