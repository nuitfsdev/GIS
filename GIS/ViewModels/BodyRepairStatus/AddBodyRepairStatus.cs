using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.BodyRepairStatus
{
    public class AddBodyRepairStatus
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime FinishDate { get; set; }
        [Required]
        public string RepairReason { get; set; } = string.Empty;
        [Required]
        public Guid AccountId { get; set; }
        [Required]
        public Guid DamageReportId { get; set; }
        [Required]
        public Guid BodyId { get; set; }
    }
}
