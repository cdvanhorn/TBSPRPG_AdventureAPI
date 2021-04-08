using AdventureApi.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using System.Linq;

namespace AdventureApi.Repositories {
    public interface ILocationRepository {
        Task<Location> GetInitialForAdventure(Guid id);
    }

    public class LocationRepository : ILocationRepository {
        private readonly AdventureContext _context;

        public LocationRepository(AdventureContext context) {
            _context = context;
        }

        public Task<Location> GetInitialForAdventure(Guid adventureId) {
            //var locations = _context.Locations.FirstOrDefaultAsync();
            var locations = from loc in _context.Locations.AsQueryable()
                            where loc.AdventureId == adventureId
                            where loc.Initial
                            select loc;
            return locations.FirstOrDefaultAsync();
        }
    }
}