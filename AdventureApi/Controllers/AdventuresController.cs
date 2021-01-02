using AdventureApi.Services;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Collections.Generic;

using AdventureApi.Entities;

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
            return Ok(adventures);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name) {
            var adventure = await _adventuresService.GetByName(name);
            return Ok(adventure);
        }

        //[Authorize]
        [Route("initiallocation/{id}")]
        public IActionResult GetInitialLocation(string id) {
            _locationService.GetInitialForLocation(id);
            //return Ok(_locationService.GetInitialForLocation(id));
            return Ok("banana");
        }
    }
}