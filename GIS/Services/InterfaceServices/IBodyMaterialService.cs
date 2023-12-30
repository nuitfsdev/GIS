using GIS.Models;
using GIS.Services.CRUDServices;

namespace GIS.Services.InterfaceServices
{
    public interface IBodyMaterialService : ICRUDService<BodyMaterial>
    {
        public Task<bool> DeleteBodyMaterial(Guid bodyId, Guid materialId);
    }
}
