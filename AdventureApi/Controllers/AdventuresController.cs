using AdventureApi.Services;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Linq;

using AdventureApi.Entities;
using AdventureApi.ViewModels;

using System;

namespace AdventureApi.Controllers {

    [ApiController]
    [Route("/api/[controller]")]
    public class AdventuresController : ControllerBase {
        IAdventuresService _adventuresService;
        ILocationService _locationService;

        public AdventuresController(IAdventuresService adventuresService, ILocationService locationService) {
            _adventuresService = adventuresService;
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var adventures = await _adventuresService.GetAll();
            return Ok(adventures.Select(adv => new AdventureViewModel(adv)).ToList());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name) {
            var adventure = await _adventuresService.GetByName(name);
            if(adventure == null)
                return BadRequest(new { message = "invalid adventure name" });
            return Ok(new AdventureViewModel(adventure));
        }

        //[Authorize]
        [Route("initiallocation/{id}")]
        public async Task<IActionResult> GetInitialLocation(int id) {
            Location loc = await _locationService.GetInitialForLocation(id);
            if(loc == null)
                return BadRequest(new { message = "invalid game id" });
            return Ok(new LocationViewModel(loc));
        }
    }
}