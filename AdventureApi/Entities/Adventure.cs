using System.Collections.Generic;
using System;

namespace AdventureApi.Entities {
    public class Adventure {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public Guid SourceKey { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}