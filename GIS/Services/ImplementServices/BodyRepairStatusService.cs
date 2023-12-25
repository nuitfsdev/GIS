using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class BodyRepairStatusService : CRUDService<BodyRepairStatus>, IBodyRepairStatusService
    {
        public BodyRepairStatusService(DatabaseContext context) : base(context)
        {
        }
    }
}
