using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.BodyComp
{
    public class UpdateBodyComp
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public double Width { get; set; }
        public string Material { get; set; } = string.Empty;
    }
}
