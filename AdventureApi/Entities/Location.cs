using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AdventureApi.Entities {
    public class Location {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("initial")]
        public bool Initial { get; set; }
    }
}