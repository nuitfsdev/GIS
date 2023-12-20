using GIS.Database;
using GIS.Models;
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
        private readonly DatabaseContext _db;
        public FeedbacksController(DatabaseContext db) {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _db.Feedbacks.ToList();
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _db.Feedbacks.FindAsync(id);
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
            await _db.Feedbacks.AddAsync(fb);
            await _db.SaveChangesAsync();

            return Ok(fb);
        }

        
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AddFeedback updateFeedback)
        {
            Feedback? fb = await _db.Feedbacks.FindAsync(id);
            if (fb == null)
            {
                return NotFound();
            }
            fb.Name = updateFeedback.Name;
            fb.Email = updateFeedback.Email;
            fb.Sdt = updateFeedback.Sdt;
            fb.Message = updateFeedback.Message;

            await _db.SaveChangesAsync();

            return Ok(fb);
        }

        
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Feedback? fb = await _db.Feedbacks.FindAsync(id);
            if (fb == null)
            {
                return NotFound();
            }

            _db.Feedbacks.Remove(fb);
            await _db.SaveChangesAsync();

            return Ok(fb);
        }
    }
}
