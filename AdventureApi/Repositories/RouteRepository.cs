using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdventureApi.Repositories
{
    public interface IRouteRepository
    {
        Task<List<Route>> GetRoutesForLocation(Guid locationId);
    }
    
    public class RouteRepository : IRouteRepository
    {
        private readonly AdventureContext _context;

        public RouteRepository(AdventureContext context)
        {
            _context = context;
        }

        public Task<List<Route>> GetRoutesForLocation(Guid locationId)
        {
            return _context.Routes.AsQueryable().Where(rt => rt.LocationId == locationId).ToListAsync();
        }
    }
}