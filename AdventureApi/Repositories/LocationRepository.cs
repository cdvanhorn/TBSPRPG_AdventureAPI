using AdventureApi.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using TbspRpgLib.Settings;
using TbspRpgLib.Repositories;

namespace AdventureApi.Repositories {
    public interface ILocationRepository {
        Task<Location> GetInitialForAdventure(int id);
    }

    public class LocationRepository : ILocationRepository {
        private AdventureContext _context;

        public LocationRepository(AdventureContext context) {
            _context = context;
        }

        public Task<Location> GetInitialForAdventure(int adventureId) {
            //var locations = _context.Locations.FirstOrDefaultAsync();
            var locations = from loc in _context.Locations.AsQueryable()
                            where loc.AdventureId == adventureId
                            where loc.Initial
                            select loc;
            return locations.FirstOrDefaultAsync();
        }
    }
}