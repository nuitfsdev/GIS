using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.Body
{
    public class AddBodyComp
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Path { get; set; } = string.Empty;
        [Required]
        public string Color { get; set; } = string.Empty;
        [Required]
        public double Width { get; set; }
    }
}
