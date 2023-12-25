using GIS.Models;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.BodyMaterial;
using GIS.ViewModels.BodyRepairStatus;
using GIS.ViewModels.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyRepairStatusController : ControllerBase
    {
        private readonly IBodyRepairStatusService _bodyRepairStatusService;
        private readonly IDamageReportService _damageReportService;
        private readonly IBodyService _bodyService;
        public BodyRepairStatusController(IBodyRepairStatusService bodyRepairStatusService,
            IDamageReportService damageReportService, IBodyService bodyService)
        {
            _bodyRepairStatusService = bodyRepairStatusService;
            _damageReportService = damageReportService;
            _bodyService = bodyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bodyRepairStatusService.ReadAllAsync(e => true));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _bodyRepairStatusService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBodyRepairStatus addBodyRepairStatus)
        {
            //DateTime utcDateTime = DateTimeOffset.Parse(addBodyRepairStatus.StartDate).UtcDateTime;
            BodyRepairStatus bodyRepairStatus = new()
            {
                StartDate = addBodyRepairStatus.StartDate,
                FinishDate = addBodyRepairStatus.FinishDate,
                RepairReason = addBodyRepairStatus.RepairReason,
                AccountId = addBodyRepairStatus.AccountId,
                DamageReportId = addBodyRepairStatus.DamageReportId,
                BodyId = addBodyRepairStatus.BodyId
            };

            return Ok(await _bodyRepairStatusService.CreateAsync(bodyRepairStatus));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AddBodyRepairStatus updateBodyRepairStatus)
        {
            BodyRepairStatus? bodyRepairStatus = await _bodyRepairStatusService.ReadByIdAsync(id);
            if (bodyRepairStatus == null)
            {
                return NotFound();
            }
            bodyRepairStatus.StartDate = updateBodyRepairStatus.StartDate;
            bodyRepairStatus.FinishDate = updateBodyRepairStatus.FinishDate;
            bodyRepairStatus.RepairReason = updateBodyRepairStatus.RepairReason;
            bodyRepairStatus.AccountId = updateBodyRepairStatus.AccountId;
            bodyRepairStatus.DamageReportId = updateBodyRepairStatus.DamageReportId;
            return Ok(await _bodyRepairStatusService.UpdateAsync(bodyRepairStatus));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            BodyRepairStatus? bodyRepairStatus = await _bodyRepairStatusService.ReadByIdAsync(id);
            if (bodyRepairStatus == null)
            {
                return NotFound();
            }
            return Ok(await _bodyRepairStatusService.DeleteAsync(id));
        }
    }
}
