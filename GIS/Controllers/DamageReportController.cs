using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.BodyMaterial;
using GIS.ViewModels.DamageReport;
using GIS.ViewModels.Material;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DamageReportController : ControllerBase
    {
        private readonly IDamageReportService _damageReportService;

        public DamageReportController(IDamageReportService damageReportService)
        {
            _damageReportService = damageReportService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _damageReportService.ReadAllAsync(e => true));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _damageReportService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddDamageReport addDamageReport)
        {
            DamageReport damageReport = new()
            {
                Date = DateTime.UtcNow,
                Content = addDamageReport.Content,
                Cause = addDamageReport.Cause,
                BodyId = addDamageReport.BodyId,
                AccountId = addDamageReport.AccountId
            };

            return Ok(await _damageReportService.CreateAsync(damageReport));
        }
        // api này dùng để thêm damegereport với date tự nhập theo dạng chuỗi như này: 2021-10-08T04:50:12.0000000
        [HttpPost("try")]
        public async Task<IActionResult> Post2([FromBody] AddDamageReport2 addDamageReport2)
        {
            DateTime utcDateTime = DateTimeOffset.Parse(addDamageReport2.Date).UtcDateTime;
            DamageReport damageReport = new()
            {
                Date = utcDateTime,
                Content = addDamageReport2.Content,
                Cause = addDamageReport2.Cause,
                BodyId = addDamageReport2.BodyId,
                AccountId = addDamageReport2.AccountId
            };

            return Ok(await _damageReportService.CreateAsync(damageReport));
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AddDamageReport2 updateDamageReport)
        {
            DamageReport? damageReport = await _damageReportService.ReadByIdAsync(id);
            if (damageReport == null)
            {
                return NotFound();
            }

            if (!DateTimeOffset.TryParse(updateDamageReport.Date, out DateTimeOffset parsedDate))
            {
                return BadRequest("Invalid date format");
            }

            damageReport.Date = parsedDate.UtcDateTime;
            damageReport.Content = updateDamageReport.Content;
            damageReport.Cause = updateDamageReport.Cause;
            damageReport.BodyId = updateDamageReport.BodyId;
            damageReport.AccountId = updateDamageReport.AccountId;

            return Ok(await _damageReportService.UpdateAsync(damageReport));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            DamageReport? damageReport = await _damageReportService.ReadByIdAsync(id);
            if (damageReport == null)
            {
                return NotFound();
            }
            return Ok(await _damageReportService.DeleteAsync(id));
        }

        [HttpPut("{id:Guid}/status/accept")]
        public async Task<IActionResult> Put(Guid id)
        {
            DamageReport? damageReport = await _damageReportService.ReadByIdAsync(id);
            if (damageReport == null)
            {
                return NotFound();
            }

            damageReport.Status = "Đã chấp nhận";

            return Ok(await _damageReportService.UpdateAsync(damageReport));
        }
    }
}
