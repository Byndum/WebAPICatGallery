using MongoDB.Bson.Serialization.Attributes;

namespace WebAPICatGallery
{
    public class Cat
    {
        [BsonId]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Breed { get; set; }
    }
}
