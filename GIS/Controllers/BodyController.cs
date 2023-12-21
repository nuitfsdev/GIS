using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Body;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyController : ControllerBase
    {
        private readonly IPrismService _prismService;

        public BodyController(IPrismService prismService)
        {
            _prismService = prismService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _prismService.ReadAllAsync(e => true));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBody addBody)
        {
            Prism prism = new ()
            {
                Name = addBody.Name,
                Path = addBody.Path,
                Color = addBody.Color,
                Height = addBody.Height
            };

            return Ok(await _prismService.CreateAsync(prism));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Prism? sample = await _prismService.ReadByIdAsync(id);
            if (sample == null)
            {
                return NotFound();
            }
            return Ok(await _prismService.DeleteAsync(id));
        }
    }
}
