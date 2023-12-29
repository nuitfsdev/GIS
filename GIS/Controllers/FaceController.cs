using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Face;
using GIS.ViewModels.Material;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UpdateFace addFace)
        {
            Face face = new()
            {
                 Path = addFace.Path,
            };
            return Ok(await _faceService.CreateAsync(face));
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

        [HttpPut("generalPath")]
        public async Task<IActionResult> PutGeneralPath(string path, [FromBody] UpdateFace updateFace)
        {
            IEnumerable<Face> faces = await _faceService.ReadAllAsync(e => true);
            var filteredFaces = faces.Where(face => face.Path.Contains(path));
            if (filteredFaces.Count() == 0)
            {
                return NotFound();
            }

            for(int i = 0; i < filteredFaces.Count(); i++)
            {
                Face face = filteredFaces.First(face => face.Path.Contains($"/{i}"));
                face.Path = string.Concat(path, $"/{i}");
                await _faceService.UpdateAsync(face);
            }

            return Ok("Success");
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
