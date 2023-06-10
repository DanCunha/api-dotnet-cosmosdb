using Azure.Messaging.ServiceBus;
using ChallengeAPI.AzureServices;
using ChallengeAPI.Service;
using ChallengeReceiver.AzureService;
using System;
using System.Threading.Tasks;

namespace ChallengeReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductQueueReceiver queue = new ProductQueueReceiver();
            Task.Run(async () =>
            {
                await queue.ReceiveMessage();
            });
            Console.WriteLine("Fim");
            Console.Read();
        }
    }
}