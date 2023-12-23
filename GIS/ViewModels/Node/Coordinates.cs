using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.Node
{
    public class Coordinates
    {
        [Required]
        public List<List<List<List<Double>>>> nodeData { get; set; }
    }
}
