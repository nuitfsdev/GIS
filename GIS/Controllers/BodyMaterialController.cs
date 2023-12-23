using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.BodyMaterial;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyMaterialController : ControllerBase
    {
        private readonly IBodyMaterialService _bodyMaterialService;

        public BodyMaterialController(IBodyMaterialService bodyMaterialService)
        {
            _bodyMaterialService = bodyMaterialService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bodyMaterialService.ReadAllAsync(e => true));
        }

        [HttpGet("findOne")]
        public async Task<IActionResult> GetBy2Id(Guid BodyId, Guid MaterialId)
        {
            return Ok(await _bodyMaterialService.FindBy2Id(BodyId, MaterialId));
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
        public async Task<IActionResult> Delete([FromQuery] Guid BodyId, [FromQuery] Guid MaterialId)
        {
            var result = await _bodyMaterialService.DeleteBmBy2Id(BodyId, MaterialId);
            if (result == false)
                return NotFound();
            return Ok(result);
        }
    }
}
