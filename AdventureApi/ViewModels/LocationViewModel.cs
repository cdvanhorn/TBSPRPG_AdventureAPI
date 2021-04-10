using System;
using AdventureApi.Entities;

namespace AdventureApi.ViewModels {
    public class LocationViewModel {
        public Guid Id { get; set; }

        public Guid AdventureId { get; set; }

        public LocationViewModel() {}

        public LocationViewModel(Location location) {
            Id = location.Id;
            AdventureId = location.AdventureId;
        }
    }
}