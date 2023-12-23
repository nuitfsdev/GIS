using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class BodyCompService : CRUDService<BodyComp>, IBodyCompService
    {
        public BodyCompService(DatabaseContext context) : base(context)
        {
        }
    }
}
