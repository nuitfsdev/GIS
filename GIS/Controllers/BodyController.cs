using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Body;
using GIS.ViewModels.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyController : ControllerBase
    {
        private readonly IPrismService _prismService;
        private readonly IBodyCompService _bodyCompService;

        public BodyController(IPrismService prismService, IBodyCompService bodyCompService)
        {
            _prismService = prismService;
            _bodyCompService = bodyCompService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _prismService.ReadAllAsync(e => true));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _bodyCompService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // Get geojson ...
        [HttpGet("path")]
        public async Task<IActionResult> GetGeojsonObject()
        {
            return Ok(await _prismService.ReadAllAsync(e => true));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBody addBody)
        {
            BodyComp? bodyComp = null;
            Prism? prism = null;
            if (addBody.Width == null)
            {
                prism = new()
                {
                    Name = addBody.Name,
                    Path = addBody.Path,
                    Color = addBody.Color,
                    Height = addBody.Height
                };
            }
            else
            {
                bodyComp = new()
                {
                    Name = addBody.Name,
                    Path = addBody.Path,
                    Color = addBody.Color,
                    Width = addBody.Width
                };
            }
            if (prism != null)
                return Ok(await _prismService.CreateAsync(prism));
            return bodyComp != null ? Ok(await _bodyCompService.CreateAsync(bodyComp))
                : Ok(null);
        }

        [HttpPost("prism")]
        public async Task<IActionResult> Post([FromBody] AddPrism addPrism)
        {
            Prism prism = new()
            {
                Name = addPrism.Name,
                Path = addPrism.Path,
                Color = addPrism.Color,
                Height = addPrism.Height
            };
            return Ok(await _prismService.CreateAsync(prism));
        }

        [HttpPost("body-comp")]
        public async Task<IActionResult> Post([FromBody] AddBodyComp addBodyComp)
        {
            BodyComp bodyComp = new()
            {
                Name = addBodyComp.Name,
                Path = addBodyComp.Path,
                Color = addBodyComp.Color,
                Width = addBodyComp.Width
            };
            return Ok(await _bodyCompService.CreateAsync(bodyComp));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrism(Guid id)
        {
            Prism? prism = await _prismService.ReadByIdAsync(id);
            BodyComp? bodyComp = await _bodyCompService.ReadByIdAsync(id);

            if (prism == null && bodyComp == null)
            {
                return NotFound();
            }
            return prism == null ? Ok(await _bodyCompService.DeleteAsync(id))
                : Ok(await _prismService.DeleteAsync(id));
        }

        [HttpDelete("body-comp/{id}")]
        public async Task<IActionResult> DeleteBodyComp(Guid id)
        {
            BodyComp? bodyComp = await _bodyCompService.ReadByIdAsync(id);
            if (bodyComp == null)
            {
                return NotFound();
            }
            return Ok(await _bodyCompService.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AddBody updateBody)
        {

            BodyComp? bodyComp = await _bodyCompService.ReadByIdAsync(id);
            Prism? prism = await _prismService.ReadByIdAsync(id);
            if (bodyComp == null && prism == null)
            {
                return NotFound();
            }
            else if (bodyComp != null)
            { 
                bodyComp.Name = updateBody.Name;
                bodyComp.Path = updateBody.Path;
                bodyComp.Color = updateBody.Color;
                bodyComp.Width = updateBody.Width;
            } 
            else if (prism != null) 
            {
                prism.Name = updateBody.Name;
                prism.Path = updateBody.Path;
                prism.Color = updateBody.Color;
                prism.Height = updateBody.Height;
            }
 

            return prism == null ? Ok(await _bodyCompService.UpdateAsync(bodyComp))
                : Ok(await _prismService.UpdateAsync(prism));
        }
    }
}
