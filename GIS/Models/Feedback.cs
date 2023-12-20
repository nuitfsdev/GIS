using GIS.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace GIS.Models
{
    public class Feedback : BaseModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Sdt { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

    }
}
