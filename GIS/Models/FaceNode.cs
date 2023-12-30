using GIS.Models.BaseModels;

namespace GIS.Models
{
    public class FaceNode : BaseModel
    {
        public Guid FaceId { get; set; }
        public Guid NodeId { get; set; }
        public Face Face { get; set; } = null;
        public Node Node { get; set; } = null;
    }
}
