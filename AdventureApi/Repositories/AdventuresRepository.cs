using AdventureApi.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using TbspRpgLib.Settings;
using TbspRpgLib.Repositories;

namespace AdventureApi.Repositories {
    public interface IAdventuresRepository {
        Task<List<Adventure>> GetAllAdventures();
        Task<Adventure> GetAdventureByName(string name);
        ValueTask<Adventure> GetAdventureById(int id);
    }

    public class AdventuresRepository : IAdventuresRepository {
        private AdventureContext _context;

        public AdventuresRepository(AdventureContext context) {
            _context = context;
        }

        public Task<List<Adventure>> GetAllAdventures() {
            return _context.Adventures.AsQueryable().ToListAsync<Adventure>();
        }

        public ValueTask<Adventure> GetAdventureById(int id) {
            return _context.Adventures.FindAsync(id);
        }

        public Task<Adventure> GetAdventureByName(string name) {
            var adventures = from adventure in _context.Adventures.AsQueryable()
                            where adventure.Name.ToLower() == name.ToLower()
                            select adventure;
            return adventures.FirstOrDefaultAsync<Adventure>();
        }
    }
}