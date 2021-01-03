using AdventureApi.Repositories;

using AdventureApi.Entities;

using System.Collections.Generic;

namespace AdventureApi.Services {
    public interface ILocationService {
        IAsyncEnumerable<Location> GetInitialForLocation(string id);
    }

    public class LocationService : ILocationService{
        private ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository) {
            _locationRepository = locationRepository;
        }

        public IAsyncEnumerable<Location> GetInitialForLocation(string id) {
            return null;
        }
    }
}