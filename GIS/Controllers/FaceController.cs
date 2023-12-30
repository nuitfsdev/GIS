using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Face;
using GIS.ViewModels.Sample;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceController : ControllerBase
    {
        private readonly IFaceService _faceService;

        public FaceController(IFaceService faceService)
        {
            _faceService = faceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _faceService.ReadAllAsync(e => true));
        }

        [HttpPut("path")]
        public async Task<IActionResult> Put(string path, [FromBody] UpdateFace updateFace)
        {
            IEnumerable<Face> faces = await _faceService.ReadAllAsync(e => true);
            Face? foundFace = faces.FirstOrDefault(face => face.Path == path);

            if (foundFace == null)
            {
                return NotFound();
            }
            foundFace.Path = updateFace.Path;
            return Ok(await _faceService.UpdateAsync(foundFace));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Face? face = await _faceService.ReadByIdAsync(id);
            if (face == null)
            {
                return NotFound();
            }
            return Ok(await _faceService.DeleteAsync(id));
        }
    }
}
