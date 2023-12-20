using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Sample;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        private readonly ISampleService _sampleService;
        public SamplesController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }
        // GET: api/Samples
        [HttpGet]
        public  async Task<IActionResult> Get()
        {
            return Ok(await _sampleService.ReadAllAsync(e=>true));
        }

        // GET: api/Samples/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _sampleService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Samples
        [HttpPost]
        public async  Task<IActionResult> Post([FromBody] AddSample addSample)
        {
            Sample sample = new()
            {
                Name = addSample.Name,
                Description = addSample.Description
            };
            return Ok(await _sampleService.CreateAsync(sample));
        }

        // PUT: api/Samples/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AddSample updateSample)
        {
            Sample? sample = await _sampleService.ReadByIdAsync(id);
            if(sample == null)
            {
                return NotFound();
            }
            sample.Name = updateSample.Name;
            sample.Description = updateSample.Description;
            return Ok(await _sampleService.UpdateAsync(sample));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Sample? sample = await _sampleService.ReadByIdAsync(id);
            if(sample == null)
            {
                   return NotFound();
            }
            return Ok(await _sampleService.DeleteAsync(id));
        }
    }
}
