using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class BodyService : CRUDService<Body>, IBodyService
    {
        public BodyService(DatabaseContext context) : base(context)
        {

        }
    }
}
