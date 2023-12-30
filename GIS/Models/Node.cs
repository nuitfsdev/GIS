using GIS.Models.BaseModels;

namespace GIS.Models
{
    public class Node : BaseModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public ICollection<FaceNode> FaceNode { get; } = new List<FaceNode>();
    }
}
