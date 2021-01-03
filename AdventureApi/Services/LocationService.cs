using AdventureApi.Repositories;

using AdventureApi.Entities;

using System.Threading.Tasks;

namespace AdventureApi.Services {
    public interface ILocationService {
        Task<Location> GetInitialForLocation(int id);
    }

    public class LocationService : ILocationService{
        private ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository) {
            _locationRepository = locationRepository;
        }

        public Task<Location> GetInitialForLocation(int id) {
            return _locationRepository.GetInitialForAdventure(id);
        }
    }
}