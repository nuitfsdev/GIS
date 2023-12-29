using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.FaceNode
{
    public class AddFaceNode
    {
        [Required]
        public Guid FaceId { get; set; }
        [Required]
        public Guid NodeId { get; set; }
    }
}
