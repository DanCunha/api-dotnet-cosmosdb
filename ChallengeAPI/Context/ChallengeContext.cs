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
        private static readonly string accountEndpoint = "https://challenge-db.documents.azure.com:443/";
        private string? accountKey = Environment.GetEnvironmentVariable("AZURE_COSMOS_DB_KEY");

        public async Task<Container> GetContainer() 
        {

            var client = new CosmosClientBuilder(accountEndpoint, accountKey).Build();
            database = await client.CreateDatabaseIfNotExistsAsync(dataBaseId);

            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/partitionKey");


            return container = client.GetContainer(dataBaseId, containerId);

        }   
    }
}
