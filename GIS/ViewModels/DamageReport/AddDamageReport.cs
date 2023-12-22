using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.DamageReport
{
    public class AddDamageReport
    {
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public string Cause { get; set; } = string.Empty;
        [Required]
        public Guid BodyId { get; set; }
        [Required]
        public Guid AccountId { get; set; }
    }
}
