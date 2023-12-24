using GIS.Services.InterfaceServices;
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
    }
}
