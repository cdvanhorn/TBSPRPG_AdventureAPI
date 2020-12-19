using AdventureApi.Entities;

using MongoDB.Driver;

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
            var location = from adv in _adventures.AsQueryable()
                            from loc in adv.Locations
                            where adv.Id == id
                            where loc.Initial
                            select loc;
            return location.ToAsyncEnumerable();
        }
    }
}