using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.Material
{
    public class AddMaterial
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
