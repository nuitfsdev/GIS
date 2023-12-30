using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.FaceNode
{
    public class AddFaceAndNode
    {
        [Required]
        public string GeneralPath { get; set; } = string.Empty;
        [Required]
        public List<List<List<List<Double>>>> nodeData { get; set; }
    }
}
