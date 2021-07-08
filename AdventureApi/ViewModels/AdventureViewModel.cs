using System;
using AdventureApi.Entities;

namespace AdventureApi.ViewModels {
    public class AdventureViewModel {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public Guid SourceKey { get; set; }

        public AdventureViewModel() {}

        public AdventureViewModel(Adventure adventure) {
            Id = adventure.Id;
            Name = adventure.Name;
            SourceKey = adventure.SourceKey;
        }
    }
}