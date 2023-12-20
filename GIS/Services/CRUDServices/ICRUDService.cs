using GIS.Models.BaseModels;

namespace GIS.Services.CRUDServices
{
    public interface ICRUDService<T> where T : class, IBaseModel
    {
        Task<T> CreateAsync(T model);

        Task<T?> ReadAsync(Guid id);

        Task<IEnumerable<T>> ReadAllAsync();

        Task<T> UpdateAsync(T model);

        Task<bool> DeleteAsync(Guid id);
    }
}