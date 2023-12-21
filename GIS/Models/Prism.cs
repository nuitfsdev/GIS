using System.ComponentModel.DataAnnotations;

namespace GIS.Models
{
    public class Prism : Body
    {
        [Required]
        public double Height { get; set; }
    }
}
