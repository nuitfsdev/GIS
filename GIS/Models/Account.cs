using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace GIS.Models
{
    public class Account : IdentityUser<Guid>
    {
      public string Name { get; set; } = string.Empty;
      public string Phone { get; set; } = string.Empty;
      public ICollection<DamageReport> DamageReports { get; } = new List<DamageReport>();
    }
}