using AdventureApi.Entities;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using TbspRpgLib.Settings;

namespace AdventureApi.Repositories {
    public interface ILocationRepository {
        IAsyncEnumerable<Location> GetInitialForAdventure(string id);
    }

    public class LocationRepository : ILocationRepository {
        private readonly IDatabaseSettings _dbSettings;

        private readonly IMongoCollection<Adventure> _adventures;

        public LocationRepository(IDatabaseSettings databaseSettings) {
            _dbSettings = databaseSettings;

            var connectionString = $"mongodb+srv://{_dbSettings.Username}:{_dbSettings.Password}@{_dbSettings.Url}/{_dbSettings.Name}?retryWrites=true&w=majority";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(_dbSettings.Name);

            _adventures = database.GetCollection<Adventure>("adventures");
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