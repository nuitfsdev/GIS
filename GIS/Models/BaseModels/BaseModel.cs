namespace GIS.Models.BaseModels
{
    public class BaseModel : IBaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
