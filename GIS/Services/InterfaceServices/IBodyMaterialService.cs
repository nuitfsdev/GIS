using GIS.Models;
using GIS.Services.CRUDServices;

namespace GIS.Services.InterfaceServices
{
    public interface IBodyMaterialService : ICRUDService<BodyMaterial>
    {
        public Task<bool> DeleteBmBy2Id(Guid BodyId, Guid MaterialId);
        public Task<BodyMaterial?> FindBy2Id(Guid BodyId, Guid MaterialId); 
    }
}
