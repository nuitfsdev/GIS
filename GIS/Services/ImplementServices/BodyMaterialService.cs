using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class BodyMaterialService : CRUDService<BodyMaterial>, IBodyMaterialService
    {
        public BodyMaterialService(DatabaseContext context) : base(context) {
        }
    }
}
