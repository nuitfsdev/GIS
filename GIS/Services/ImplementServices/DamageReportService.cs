using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class DamageReportService : CRUDService<DamageReport>, IDamageReportService
    {
        public DamageReportService(DatabaseContext context) : base(context)
        {
        }
    }
}
