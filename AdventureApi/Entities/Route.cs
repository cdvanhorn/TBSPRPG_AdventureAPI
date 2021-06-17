using System;

namespace AdventureApi.Entities
{
    public class Route
    {
        public Guid Id { get; set; }
        
        public Guid LocationId { get; set; }
        
        public Location Location { get; set; }
        
        public Guid SourceId { get; set; }
        
        public string Name { get; set; }
    }
}