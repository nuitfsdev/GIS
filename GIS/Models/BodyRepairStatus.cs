using GIS.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace GIS.Models
{
    public class BodyRepairStatus : BaseModel
    {
        [Required]
        public System.DateTime StartDate { get; set; }
        public System.DateTime FinishDate { get; set; }
        public string RepairReason { get; set; } = string.Empty;
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public Guid DamageReportId { get; set; }
        public DamageReport DamageReport { get; set; }
        public Guid BodyId { get; set; }
        public Body Body { get; set; }
    }
}
