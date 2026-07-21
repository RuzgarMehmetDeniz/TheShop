using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheShop.Catalog.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string ProductName {  get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public string CategoryId { get; set; }
    }
}
