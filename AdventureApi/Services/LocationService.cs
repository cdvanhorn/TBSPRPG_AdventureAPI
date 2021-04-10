using AdventureApi.Repositories;
using AdventureApi.ViewModels;
using AdventureApi.Entities;

using System.Threading.Tasks;
using System;

namespace AdventureApi.Services {
    public interface ILocationService {
        Task<LocationViewModel> GetInitialForLocation(Guid id);
    }

    public class LocationService : ILocationService{
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository) {
            _locationRepository = locationRepository;
        }

        public async Task<LocationViewModel> GetInitialForLocation(Guid id) {
            var location = await _locationRepository.GetInitialForAdventure(id);
            return location == null ? null : new LocationViewModel(location);
        }
    }
}