using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureApi.Entities;
using AdventureApi.Repositories;

namespace AdventureApi.Services
{
    public interface IRouteService
    {
        Task<List<Route>> GetRoutesForLocation(Guid locationId);
    }
    
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _repository;

        public RouteService(IRouteRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Route>> GetRoutesForLocation(Guid locationId)
        {
            return _repository.GetRoutesForLocation(locationId);
        }
    }
}