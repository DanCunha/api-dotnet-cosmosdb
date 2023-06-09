using AutoMapper;
using Azure.Messaging.ServiceBus;
using ChallengeAPI.Models.Dtos;
using ChallengeAPI.Models.Entities;
using Newtonsoft.Json;
using System.Text;

namespace ChallengeAPI.AzureServices
{
    public class ProductQueue
    {
        private string? _azureBusConnection = Environment.GetEnvironmentVariable("AZURE_SERVICE_BUS_CONNECTION");
        private const string _queueName = "challenge-queue";
        private readonly IMapper _mapper;   

        public ProductQueue(IMapper mapper)
        {
            _mapper = mapper;   
        }

        public async Task SendMessage(Product product)
        {
            ServiceBusClient client;
            ServiceBusSender sender;

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(_azureBusConnection, clientOptions);
            sender = client.CreateSender(_queueName);

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            // try adding a message to the batch
            if (!messageBatch.TryAddMessage(new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(productDto)))))
            {
                // if it is too large for the batch
                throw new Exception($"The message is too large to fit in the batch.");
            }
            
            try
            {
                // Use the producer client to send the batch of messages to the Service Bus queue
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine("Message has been published to the queue.");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }

        }

    }
}
