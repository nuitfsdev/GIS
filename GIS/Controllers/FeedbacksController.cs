using GIS.Database;
using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Feedback;
using GIS.ViewModels.Sample;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbacksController(IFeedbackService feedbackService) {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _feedbackService.ReadAllAsync(e => true));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _feedbackService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddFeedback addFeedback)
        {
            Feedback fb = new()
            {
                Name = addFeedback.Name,
                Email = addFeedback.Email,
                Sdt = addFeedback.Sdt,
                Message = addFeedback.Message
            };
            return Ok(await _feedbackService.CreateAsync(fb));
        }

        
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AddFeedback updateFeedback)
        {
            Feedback? fb = await _feedbackService.ReadByIdAsync(id);
            if (fb == null)
            {
                return NotFound();
            }
            fb.Name = updateFeedback.Name;
            fb.Email = updateFeedback.Email;
            fb.Sdt = updateFeedback.Sdt;
            fb.Message = updateFeedback.Message;

            return Ok(await _feedbackService.UpdateAsync(fb));
        }

        
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool isDelete = await _feedbackService.DeleteAsync(id);
            if (!isDelete)
            {
                return NotFound();
            }

            return Ok(isDelete);
        }
    }
}
