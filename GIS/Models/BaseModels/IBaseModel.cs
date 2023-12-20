namespace GIS.Models.BaseModels
{
    public interface IBaseModel
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? LastModifiedAt { get; set; }
    }
}