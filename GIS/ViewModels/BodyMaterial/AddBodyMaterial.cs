using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.BodyMaterial
{
    public class AddBodyMaterial
    {
        [Required]
        public Guid BodyId { get; set; }
        [Required]
        public Guid MaterialId {  get; set; }
    }
}
