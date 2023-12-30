using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class NodeService : CRUDService<Node>, INodeService
    {
        public NodeService(DatabaseContext context) : base(context)
        {
        }
    }
}
