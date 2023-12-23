using GIS.Models.BaseModels;

namespace GIS.Models
{
    public class Face : BaseModel
    {
        public string Path { get; set; }
        public ICollection<FaceNode> FaceNode { get; } = new List<FaceNode>();
    }
}
