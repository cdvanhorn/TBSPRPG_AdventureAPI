using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AdventureApi.Entities {
    public class Location {
        public int Id { get; set; }

        public bool Initial { get; set; }

        public int AdventureId { get; set; }

        public Adventure Adventure { get; set; }
    }
}