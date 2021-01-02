using AdventureApi.Entities;

using MongoDB.Driver;
using MongoDB.Bson.Serialization;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using TbspRpgLib.Settings;
using TbspRpgLib.Repositories;

namespace AdventureApi.Repositories {
    public interface ILocationRepository {
        IAsyncEnumerable<Location> GetInitialForAdventure(string id);
    }

    public class LocationRepository : MongoRepository, ILocationRepository {
        private readonly IMongoCollection<Adventure> _adventures;

        public LocationRepository(IDatabaseSettings databaseSettings) : base(databaseSettings) {
            _adventures = _mongoDatabase.GetCollection<Adventure>("adventures");
        }

        public IAsyncEnumerable<Location> GetInitialForAdventure(string id) {
            // var location = from adv in _adventures.AsQueryable()
            //                 from loc in adv.Locations
            //                 where adv.Id == id
            //                 where loc.Initial
            //                 select loc;

            var filter = Builders<Adventure>.Filter.Eq(adv => adv.Id, id)
                & Builders<Adventure>.Filter.Eq("Locations.Initial", true);
            var projection = Builders<Adventure>.Projection.Include("Locations");
            var adventures = _adventures.Find(filter).Project(projection);
            

            foreach(var adv in adventures.ToList()) {
                //BsonSerializer.Deserialize<Location>(adv.GetElement("locations"));
                Console.WriteLine(adv.GetElement("locations"));
            }


            // foreach(var adv in adventure.ToList()) {
            //     foreach(var location in adv.Locations) {
            //         Console.WriteLine(location.Id);
            //         Console.WriteLine(location.Initial);
            //     }
            // }
            
            // FilterDefinition<Adventure> idFilter = Builders<Adventure>.Filter.Eq(doc => doc.Id, id);
            // FilterDefinition<Adventure> elemFilter = Builders<Adventure>.Filter.ElemMatch(doc => doc.Locations,
            //     Builders<Location>.Filter.Eq(loc => loc.Initial, true)
            // );
            // var filter = Builders<Adventure>.Filter.And(idFilter, elemFilter);
            // var adventures = _adventures.Find(filter);
            // foreach(var adv in adventures.ToList()) {
            //     Console.WriteLine(adv.Locations.Count);
            // }
            return null;
        }
    }
}