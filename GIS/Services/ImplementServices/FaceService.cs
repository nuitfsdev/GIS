using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class FaceService : CRUDService<Face>, IFaceService
    {
        public FaceService(DatabaseContext context) : base(context)
        {
        }
    }
}
