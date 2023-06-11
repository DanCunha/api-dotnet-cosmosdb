using ChallengeAPI.Models.Entities;
using Microsoft.Azure.Cosmos.Linq;

namespace ChallengeAPI.Repository
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>()
            {
                new () { Id = 1, Username = "batman", Password = "123456", Role = "manager" },
                new () { Id = 2, Username = "robin", Password = "123456", Role = "employee" }
            };

            return users
                .FirstOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
