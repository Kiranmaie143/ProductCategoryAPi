
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System;


namespace ProductApi.Models
{
    [BsonIgnoreExtraElements]
    public class Product : BaseProduct,IProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Category")]
        public string Category { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }
        [BsonElement("categories")]
        public ICollection<Category> Categories { get; set; } = Array.Empty<Category>();
    }
}



