using AdventureApi.Entities;

namespace AdventureApi.ViewModels {
    public class LocationViewModel {
        public string Id { get; set; }

        public string AdventureId { get; set; }

        public LocationViewModel() {}

        public LocationViewModel(Location location) {
            Id = location.Id.ToString();
            AdventureId = location.AdventureId.ToString();
        }
    }
}