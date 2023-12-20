using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.Feedback
{
    public class AddFeedback
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Sdt { get; set; } = string.Empty;
        [Required]
        public string Message { get; set; } = string.Empty;
    }
}
