using GIS.Models.BaseModels;
using Npgsql.Internal.TypeHandlers;
using System.ComponentModel.DataAnnotations;

namespace GIS.Models
{
    public class DamageReport : BaseModel
    {
        [Required]
        public System.DateTime Date { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Cause { get; set; } = string.Empty;
        public string Status { get; set; } = "Đang xử lý";
        public Guid BodyId { get; set; }
        public Body Body{ get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public BodyRepairStatus BodyRepairStatus { get; set; }  
    }
}
