using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.Face
{
    public class UpdateFace
    {
        [Required]
        public string Path { get; set; }
    }
}
