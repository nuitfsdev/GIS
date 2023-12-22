using GIS.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace GIS.Models
{
    public class Body : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Path { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Status { get; set; } = "Vẫn còn tốt";
        public ICollection<DamageReport> DamageReports { get; } = new List<DamageReport>();
        public ICollection<BodyMaterial> BodyMaterial {  get; } = new List<BodyMaterial>();
    }
}
