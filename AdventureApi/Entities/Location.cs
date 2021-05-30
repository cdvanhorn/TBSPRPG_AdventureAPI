using System;
using System.Collections.Generic;

namespace AdventureApi.Entities {
    public class Location {
        public Guid Id { get; set; }

        public bool Initial { get; set; }

        public Guid AdventureId { get; set; }

        public Adventure Adventure { get; set; }
        
        public ICollection<Route> Routes { get; set; }
    }
}