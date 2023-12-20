using Microsoft.AspNetCore.Identity;

namespace GIS.Models
{
    public class Account : IdentityUser<Guid>
    {
      public string Name { get; set; } = string.Empty;
      public string Phone { get; set; } = string.Empty;

    }
}