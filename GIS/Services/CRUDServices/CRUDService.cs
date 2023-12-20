using GIS.Database;
using GIS.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace GIS.Services.CRUDServices
{
    public class CRUDService<T> : ICRUDService<T> where T : class, IBaseModel
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _entities;

        public CRUDService(DatabaseContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T model)
        {
            model.CreatedAt = DateTime.UtcNow;
            model.LastModifiedAt = DateTime.UtcNow;
            await _entities.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _entities.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await _entities.ToListAsync();   
        }

        public async Task<T?> ReadAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T model)
        {
            model.LastModifiedAt = DateTime.UtcNow;
            _entities.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}