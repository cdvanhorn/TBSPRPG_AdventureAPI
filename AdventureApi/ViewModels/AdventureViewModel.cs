using AdventureApi.Entities;

namespace AdventureApi.ViewModels {
    public class AdventureViewModel {
        public string Id { get; set; }

        public string Name { get; set; }

        public AdventureViewModel() {}

        public AdventureViewModel(Adventure adventure) {
            Id = adventure.Id.ToString();
            Name = adventure.Name;
        }
    }
}