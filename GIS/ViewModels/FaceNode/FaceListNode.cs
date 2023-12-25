namespace GIS.ViewModels.FaceNode
{
    public class FaceListNode
    {
        public Guid faceId { get; set; }
        public List<Guid> nodeIds {  get; set; } = new List<Guid>();
    }
}
