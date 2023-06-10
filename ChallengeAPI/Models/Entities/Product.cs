using Newtonsoft.Json;

namespace ChallengeAPI.Models.Entities
{
    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        [JsonProperty(PropertyName = "partitionKey")]
        public string? PartitionKey { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public int Qtd { get; set; }
        public bool Status { get; set; }

    }
}
