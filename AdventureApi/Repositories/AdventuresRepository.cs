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
        Task<Adventure> GetAdventureById(string id);
    }

    public class AdventuresRepository : IAdventuresRepository {
        private AdventureContext _context;

        public AdventuresRepository(AdventureContext context) {
            _context = context;
        }

        public Task<List<Adventure>> GetAllAdventures() {
            return null;
        }

        public Task<Adventure> GetAdventureById(string id) {
            return null;
        }

        public Task<Adventure> GetAdventureByName(string name) {
            return null;
        }
    }
}