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
        Task<Adventure> GetAdventureById(Guid id);
    }

    public class AdventuresRepository : IAdventuresRepository {
        private readonly AdventureContext _context;

        public AdventuresRepository(AdventureContext context) {
            _context = context;
        }

        public Task<List<Adventure>> GetAllAdventures() {
            return _context.Adventures.AsQueryable().ToListAsync();
        }

        public Task<Adventure> GetAdventureById(Guid id) {
            return _context.Adventures.AsQueryable().Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public Task<Adventure> GetAdventureByName(string name) {
            var adventures = from adventure in _context.Adventures.AsQueryable()
                            where adventure.Name.ToLower() == name.ToLower()
                            select adventure;
            return adventures.FirstOrDefaultAsync();
        }
    }
}