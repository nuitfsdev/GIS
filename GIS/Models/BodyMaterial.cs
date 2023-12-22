using GIS.Models.BaseModels;

namespace GIS.Models
{
    public class BodyMaterial : BaseModel
    {
        public DateTime? AgeStartTime { get; set; }
        public Guid BodyId { get; set; }
        public Body Body { get; set; } = null;
        public Guid MaterialId { get; set; }
        public Material Material { get; set; } = null;
    }
}
