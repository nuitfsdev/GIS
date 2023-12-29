using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.BodyMaterial;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyMaterialController : ControllerBase
    {
        private readonly IBodyMaterialService _bodyMaterialService;
        private readonly IBodyService _bodyService;
        private readonly IMaterialService _materialService;

        public BodyMaterialController(IBodyMaterialService bodyMaterialService, IBodyService bodyService, IMaterialService materialService)
        {
            _bodyMaterialService = bodyMaterialService;
            _bodyService = bodyService;
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bodyMaterialService.ReadAllAsync(e => true));
        }

        [HttpGet("ids")]
        public async Task<IActionResult> Get([FromQuery]Guid bodyId, [FromQuery] Guid materialId)
        {
            var list = await _bodyMaterialService.ReadAllAsync(e => true);
            var result = list.FirstOrDefault(x => x.BodyId ==  bodyId && x.MaterialId == materialId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("byNamePath")]
        public async Task<IActionResult> Post([FromBody]string bodyPath)
        {
            //Guid bodyId = 
            IEnumerable<Body> bodies = await _bodyService.ReadAllAsync(e => true);
            Body body = bodies.First(x => x.Path == bodyPath);
            IEnumerable<Material> materials = await _materialService.ReadAllAsync(e => true);
            Material material = materials.First(x => x.Name == body.Material);

            BodyMaterial bodyMaterial = new()
            {
                AgeStartTime = DateTime.UtcNow,
                BodyId = body.Id,
                MaterialId = material.Id
            };

            return Ok(await _bodyMaterialService.CreateAsync(bodyMaterial));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBodyMaterial addBodyMaterial)
        {
            BodyMaterial bodyMaterial = new()
            {
                AgeStartTime = DateTime.UtcNow,
                BodyId = addBodyMaterial.BodyId,
                MaterialId = addBodyMaterial.MaterialId
            };

            return Ok(await _bodyMaterialService.CreateAsync(bodyMaterial));
        }

        // api này dùng để thêm body-material với AgeStartTime tự nhập theo dạng chuỗi như này: 2021-10-08T04:50:12.0000000
        [HttpPost("try")]
        public async Task<IActionResult> Post2([FromBody] AddBodyMaterial2 addBodyMaterial2)
        {
            DateTime utcDateTime = DateTimeOffset.Parse(addBodyMaterial2.AgeStartTime).UtcDateTime;
            BodyMaterial bodyMaterial = new()
            {
                AgeStartTime = utcDateTime,
                BodyId = addBodyMaterial2.BodyId,
                MaterialId = addBodyMaterial2.MaterialId
            };

            return Ok(await _bodyMaterialService.CreateAsync(bodyMaterial));
        }

        [HttpDelete]
        [HttpDelete("ids")]
        public async Task<IActionResult> Delete([FromQuery] Guid bodyId, [FromQuery] Guid materialId)
        {
            var list = await _bodyMaterialService.ReadAllAsync(e => true);
            var result = list.FirstOrDefault(x => x.BodyId == bodyId && x.MaterialId == materialId);
            if (result == null)
            {
                return NotFound();
            }
            bool deleteResult = await _bodyMaterialService.DeleteBodyMaterial(bodyId, materialId);
            Console.WriteLine("deleteResult");
            Console.WriteLine(deleteResult);
            if (deleteResult)
            {
                return Ok("Success");
            }

            // Xử lý trường hợp lỗi nếu cần thiết
            return StatusCode(500, "Internal Server Error");
        }
    }
}
