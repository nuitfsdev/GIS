using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Body;
using GIS.ViewModels.FaceNode;
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
        private readonly IFaceNodeService _faceNodeService;
        private readonly IFaceService _faceService;
        private readonly INodeService _nodeService;
        private readonly IBodyService _bodyService;
        public BodyController(IPrismService prismService, IBodyCompService bodyCompService, IBodyService bodyService,
            IFaceNodeService faceNodeService, IFaceService faceService, INodeService nodeService)
        {
            _prismService = prismService;
            _bodyCompService = bodyCompService;
            _faceNodeService = faceNodeService;
            _faceService = faceService;
            _nodeService = nodeService;
            _bodyService = bodyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bodyService.ReadAllAsync(e => true));
        }

        [HttpGet("path")]
        public async Task<IActionResult> GetByPath([FromQuery] string path)
        {
            IEnumerable<Body> bodies = await _bodyService.ReadAllAsync(e => true);
            Body body = bodies.First(x => x.Path == path);
            return Ok(body);
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
        [HttpPut("{id}/status/repair")]
        public async Task<IActionResult> Put(Guid id)
        {
            Body? body = await _bodyService.ReadByIdAsync(id);
            if (body == null)
            {
                return NotFound();
            }
            body.Status = "Đang sửa chữa";
            return Ok(await _bodyService.UpdateAsync(body));
        }
    }
}
