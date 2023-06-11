using Azure;
using Azure.Messaging.ServiceBus;
using ChallengeAPI.Context;
using ChallengeAPI.Models.Entities;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeReceiver.AzureService
{
    public class ProductQueueReceiver
    {
        public async Task ReceiveMessage()
        {
            ServiceBusClient client;
            ServiceBusProcessor processor;

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(Environment.GetEnvironmentVariable("AZURE_SERVICE_BUS_CONNECTION"), clientOptions);

            processor = client.CreateProcessor("challenge-queue", new ServiceBusProcessorOptions());

            try
            {
                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }

        }

        // handle received messages
        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            Product product = new Product()
            {
                Id = JObject.Parse(body)["Id"].ToString(),
                PartitionKey = JObject.Parse(body)["PartitionKey"].ToString(),
                Status = true
            };

            var context = new ChallengeContext();
            var container = await context.GetContainer();

            ItemResponse<Product> productResponse = await container.ReadItemAsync<Product>(product.Id, new PartitionKey(product.PartitionKey));
            var itemBody = productResponse.Resource;
            itemBody.Status = true; 

            // replace the item with the updated content
            productResponse = await container.ReplaceItemAsync<Product>(itemBody, itemBody.Id, new PartitionKey(itemBody.PartitionKey));
            Console.WriteLine("Updated Family [{0},{1}].\n \tBody is now: {2}\n", itemBody.Name, itemBody.Id, productResponse.Resource);

            Console.WriteLine($"Received: {body}");

            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
