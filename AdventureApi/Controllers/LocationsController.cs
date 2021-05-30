using System;
using System.Linq;
using System.Threading.Tasks;
using AdventureApi.Services;
using AdventureApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdventureApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public LocationsController(IRouteService routeService)
        {
            _routeService = routeService;
        }
        
        [Authorize, Route("routes/{locationId:guid}")]
        public async Task<IActionResult> GetRoutesForLocation(Guid locationId)
        {
            var routes = await _routeService.GetRoutesForLocation(locationId);
            return Ok(
                routes.Select(route => new RouteViewModel(route))
                    .ToList());
        }
    }
}