using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class MaterialService : CRUDService<Material>, IMaterialService
    {
        public MaterialService(DatabaseContext context) : base(context)
        {
        }
    }
}
