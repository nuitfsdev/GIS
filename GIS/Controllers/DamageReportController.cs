using GIS.Models;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.DamageReport;
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
    }
}
