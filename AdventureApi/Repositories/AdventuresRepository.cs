using AdventureApi.Entities;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TbspRpgLib.Settings;
using TbspRpgLib.Repositories;

namespace AdventureApi.Repositories {
    public interface IAdventuresRepository {
        Task<List<Adventure>> GetAllAdventures();
        Task<Adventure> GetAdventureByName(string name);
    }

    public class AdventuresRepository : MongoRepository, IAdventuresRepository {
        private readonly IMongoCollection<Adventure> _adventures;

        public AdventuresRepository(IDatabaseSettings databaseSettings) : base(databaseSettings) {
            _adventures = _mongoDatabase.GetCollection<Adventure>("adventures");
        }

        public Task<List<Adventure>> GetAllAdventures() {
            return _adventures.Find(adventure => true).ToListAsync();
        }

        public Task<Adventure> GetAdventureByName(string name) {
            return _adventures.Find(adventure => adventure.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }
    }
}