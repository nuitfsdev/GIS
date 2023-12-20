using GIS.Models.BaseModels;

namespace GIS.Services.CRUDServices
{
    public interface ICRUDService<T> where T : class, IBaseModel
    {
        Task<T> CreateAsync(T model);

        Task<T?> ReadByIdAsync(Guid id);

        Task<IEnumerable<T>> ReadAllAsync(Func<T, bool> expression);

        Task<T> UpdateAsync(T model);

        Task<bool> DeleteAsync(Guid id);
    }
}