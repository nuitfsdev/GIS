using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class SampleService : CRUDService<Sample>, ISampleService
    {
        public SampleService(DatabaseContext context) : base(context)
        {

        }
    }
}
