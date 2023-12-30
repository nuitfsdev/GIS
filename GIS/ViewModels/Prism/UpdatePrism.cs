using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.Prism
{
    public class UpdatePrism
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public double Height { get; set; }
        public string Material { get; set; } = string.Empty;
    }
}
