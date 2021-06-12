using System;
using AdventureApi.Entities;

namespace AdventureApi.ViewModels
{
    public class RouteViewModel
    {
        public RouteViewModel(Route route)
        {
            Id = route.Id;
            LocationId = route.LocationId;
            Name = route.Name;
        }
        
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public string Name { get; set; }
    }
}