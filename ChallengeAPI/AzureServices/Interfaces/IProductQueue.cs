using ChallengeAPI.Models.Entities;

namespace ChallengeAPI.AzureServices.Interfaces
{
    public interface IProductQueue
    {
        Task SendMessage(Product product);
    }
}
