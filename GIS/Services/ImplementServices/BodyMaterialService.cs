using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;
using Microsoft.EntityFrameworkCore;

namespace GIS.Services.ImplementServices
{
    public class BodyMaterialService : CRUDService<BodyMaterial>, IBodyMaterialService
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<BodyMaterial> _entities;
        public BodyMaterialService(DatabaseContext context) : base(context)
        {
            _context = context;
            _entities = _context.Set<BodyMaterial>();
        }

        public async Task<bool> DeleteBodyMaterial(Guid bodyId, Guid materialId)
        {
            BodyMaterial bodyMaterial = await _entities.FirstAsync(x => x.BodyId == bodyId && x.MaterialId == materialId);
            Console.WriteLine("body Id");
            Console.WriteLine(bodyMaterial.BodyId);
            if (bodyMaterial != null)
            {
                var result = _entities.Remove(bodyMaterial);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
