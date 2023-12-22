using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Feedback;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _notificationService;
        public NotificationController(INotification notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _notificationService.ReadAllAsync(e => true));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _notificationService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool isDelete = await _notificationService.DeleteAsync(id);
            if (!isDelete)
            {
                return NotFound();
            }

            return Ok(isDelete);
        }


        /*[HttpPost]
        public async Task<IActionResult> Post()
        {
            
            return Ok(await _notificationService.CreateAsync(fb));
        }*/
    }
}
