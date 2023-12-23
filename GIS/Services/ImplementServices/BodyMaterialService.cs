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
        private readonly DbSet<BodyMaterial> _bodyMaterials;

        public BodyMaterialService(DatabaseContext context) : base(context)
        {
            _context = context;
            _bodyMaterials = _context.Set<BodyMaterial>();
        }

        public async Task<bool> DeleteBmBy2Id(Guid BodyId, Guid MaterialId)
        {
            var bodyMaterial = await _bodyMaterials.FindAsync(BodyId, MaterialId);
            if (bodyMaterial == null)
            {
                return false;
            }
            _bodyMaterials.Remove(bodyMaterial);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BodyMaterial?> FindBy2Id(Guid BodyId, Guid MaterialId)
        {
            var bodyMaterial = await _bodyMaterials.FindAsync(BodyId, MaterialId);
            return bodyMaterial;
        }
    }
}
