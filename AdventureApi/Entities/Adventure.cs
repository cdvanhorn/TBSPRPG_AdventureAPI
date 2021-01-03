using System.Collections.Generic;

namespace AdventureApi.Entities {
    public class Adventure {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}