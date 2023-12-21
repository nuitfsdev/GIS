using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.Body
{
    public class AddBody
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Path { get; set; } = string.Empty;
        [Required]
        public string Color { get; set; } = string.Empty;
        [Required]
        public double Height { get; set; }
    }
}
