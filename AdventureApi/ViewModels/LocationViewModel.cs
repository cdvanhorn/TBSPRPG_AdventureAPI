using AdventureApi.Entities;

namespace AdventureApi.ViewModels {
    public class LocationViewModel {
        public int Id { get; set; }

        public int AdventureId { get; set; }

        public LocationViewModel() {}

        public LocationViewModel(Location location) {
            Id = location.Id;
            AdventureId = location.AdventureId;
        }
    }
}