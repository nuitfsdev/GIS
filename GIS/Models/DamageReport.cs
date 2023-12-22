using GIS.Models.BaseModels;
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

        public Body Bodys{ get; set; }
        public Account Accounts { get; set; }
    }
}
