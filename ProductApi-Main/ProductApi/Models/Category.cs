using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductApi.Models
{
    [BsonIgnoreExtraElements]
    public class Category
    {
        [BsonElement("Id")]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Capacity")]
        public string Capacity { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }
      
    }
}
