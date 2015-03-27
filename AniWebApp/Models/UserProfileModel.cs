using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AniWebApp.Models
{
    public class UserProfileModel
    {
        [DisplayName("User Name")]
        [ReadOnly(true)]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        [DataType(DataType.Text)]
        [Required]
        public string FirstName { get; set; } 

        [DisplayName("Last Name")]
        [DataType(DataType.Text)]
        [Required]
        public string LastName { get; set; } 

        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }

        [DisplayName("Zip Code")]
        [DataType(DataType.PostalCode)]
        [Required]
        public int ZipCode { get; set; }

    }
}