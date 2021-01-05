using AdventureApi.Repositories;
using AdventureApi.ViewModels;
using AdventureApi.Entities;

using System.Threading.Tasks;

namespace AdventureApi.Services {
    public interface ILocationService {
        Task<LocationViewModel> GetInitialForLocation(int id);
    }

    public class LocationService : ILocationService{
        private ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository) {
            _locationRepository = locationRepository;
        }

        public async Task<LocationViewModel> GetInitialForLocation(int id) {
            var location = await _locationRepository.GetInitialForAdventure(id);
            if(location == null)
                return null;
            return new LocationViewModel(location);
        }
    }
}