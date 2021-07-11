using System;
using System.Linq;
using AdventureApi.Services;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using AdventureApi.ViewModels;

namespace AdventureApi.Controllers {

    [ApiController]
    [Route("/api/[controller]")]
    public class AdventuresController : ControllerBase {
        private readonly IAdventuresService _adventuresService;
        private readonly ILocationService _locationService;

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

        [HttpGet("{adventureId:guid}")]
        public async Task<IActionResult> GetAdventureById(Guid adventureId)
        {
            var adventure = await _adventuresService.GetById(adventureId);
            if(adventure == null)
                return BadRequest(new { message = "invalid adventure id" });
            return Ok(new AdventureViewModel(adventure));
        }

        [Authorize, Route("initiallocation/{id}")]
        public async Task<IActionResult> GetInitialLocation(Guid id) {
            var loc = await _locationService.GetInitialForLocation(id);
            if(loc == null)
                return BadRequest(new { message = "invalid game id" });
            return Ok(loc);
        }
    }
}