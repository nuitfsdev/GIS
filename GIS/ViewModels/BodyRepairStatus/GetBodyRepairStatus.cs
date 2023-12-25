using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.BodyRepairStatus
{
    public class GetBodyRepairStatus
    {
        [Required]
        public System.DateTime StartDate { get; set; }
        [Required]
        public System.DateTime FinishDate { get; set; }
        [Required]
        public string RepairReason { get; set; } = string.Empty;
        [Required]
        public Guid AccountId { get; set; }
        [Required]
        public Guid DamageReportId { get; set; }
        public string BodyName { get; set; } = string.Empty;
    }
}
