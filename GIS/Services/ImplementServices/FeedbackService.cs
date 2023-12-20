using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class FeedbackService : CRUDService<Feedback>, IFeedbackService
    {
        public FeedbackService(DatabaseContext context) : base(context)
        {
        }
    }
}
