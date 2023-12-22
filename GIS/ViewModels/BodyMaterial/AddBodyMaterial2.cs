using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.BodyMaterial
{
    public class AddBodyMaterial2
    {
        [Required]
        public string AgeStartTime { get; set; }
        [Required]
        public Guid BodyId { get; set; }
        [Required]
        public Guid MaterialId { get; set; }
    }
}
