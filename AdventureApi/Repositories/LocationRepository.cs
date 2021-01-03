using AdventureApi.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using TbspRpgLib.Settings;
using TbspRpgLib.Repositories;

namespace AdventureApi.Repositories {
    public interface ILocationRepository {
        //Task<Location> GetInitialForAdventure(string id);
    }

    public class LocationRepository : ILocationRepository {

        public LocationRepository() {
        }

        public IAsyncEnumerable<Location> GetInitialForAdventure(string id) {
            return null;
        }
    }
}