using ChallengeAPI.Models.Entities;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace ChallengeAPI.AzureServices
{
    public class ProductQueue
    {
        private string? _azureBusConnection = Environment.GetEnvironmentVariable("AZURE_SERVICE_BUS_CONNECTION");

        private const string _queueName = "challenge-queue";

        public async Task SendMessage(Product product)
        {
            try
            {
                IQueueClient queueClient = new QueueClient(_azureBusConnection, _queueName);
                var orderMessage = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(product)))
                {
                    MessageId = Guid.NewGuid().ToString(),
                    ContentType = "application/json"
                };
                Console.WriteLine("Enviando para Fila");
                await queueClient.SendAsync(orderMessage).ConfigureAwait(false);
                Console.WriteLine("Enviado para fila");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
