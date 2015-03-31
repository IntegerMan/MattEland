using System.ComponentModel.DataAnnotations;

namespace AniWebApp.Models.Accounts
{
    public abstract class NewUserViewModelBase
    {
        
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }

    }
}