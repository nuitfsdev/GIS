using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class NotificationService : CRUDService<Notification>, INotification
    {
        public NotificationService(DatabaseContext context) : base(context)
        {
        }
    }
}
