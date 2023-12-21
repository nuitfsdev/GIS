using System.ComponentModel.DataAnnotations;

namespace GIS.Models
{
    public class BodyComp : Body
    {
        [Required]
        public double width { get; set; }
    }
}
