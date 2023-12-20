using GIS.Models;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Feedback;
using GIS.ViewModels.Material;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _materialService.ReadAllAsync(e => true));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _materialService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddMaterial addMaterial)
        {
            Material material = new()
            {
                Name = addMaterial.Name,
                Description = addMaterial.Description
            };
            return Ok(await _materialService.CreateAsync(material));
        }


        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AddMaterial updateMaterial)
        {
            Material? material = await _materialService.ReadByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            material.Name = updateMaterial.Name;
            material.Age = updateMaterial.Age;
            material.Description = updateMaterial.Description;

            return Ok(await _materialService.UpdateAsync(material));
        }


        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool isDelete = await _materialService.DeleteAsync(id);
            if (!isDelete)
            {
                return NotFound();
            }

            return Ok(isDelete);
        }
    }
}
