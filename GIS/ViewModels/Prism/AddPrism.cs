using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.Prism
{
    public class AddPrism
    {

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Path { get; set; } = string.Empty;
        [Required]
        public string Color { get; set; } = string.Empty;
        [Required]
        public double Height { get; set; }
        [Required]
        public string Material {  get; set; } = "Xi măng";
    }
}
