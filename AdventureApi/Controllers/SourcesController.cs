using System;
using System.Threading.Tasks;
using AdventureApi.Services;
using AdventureApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdventureApi.Controllers
{
    [ApiController, Route("/api/[controller]")]
    public class SourcesController : ControllerBase
    {
        private readonly ISourceService _sourceService;
        
        public SourcesController(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }
        
        [Authorize, HttpGet("{language}/{sourceKey:guid}")]
        public async Task<IActionResult> GetSourceContent(string language, Guid sourceKey)
        {
            try
            {
                var source = await _sourceService.GetSourceForKey(sourceKey, language);
                return Ok(new SourceViewModel()
                {
                    Key = sourceKey,
                    Language = language,
                    Source = source
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}