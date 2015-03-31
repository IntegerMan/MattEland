using System.ComponentModel.DataAnnotations;

namespace AniWebApp.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}